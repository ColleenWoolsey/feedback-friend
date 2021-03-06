﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedbackFriend.Data;
using FeedbackFriend.Models;
using Microsoft.AspNetCore.Identity;

namespace FeedbackFriend.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        //access the currently authenticated user
        private readonly UserManager<ApplicationUser> _userManager;

        //inject the UserManager service in the constructor
        public AnswersController(ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // !!!  Any method that needs to see who the user is can invoke the method
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // ************************************************************************  INDEX
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Answers.Include(a => a.Question);
            return View(await applicationDbContext.ToListAsync());
        }

        // ************************************************************************  RESULTS
        public async Task<IActionResult> Results(int id) //SurveyId
        {
            var model = new ResultsViewModel();
            if (model == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return NotFound();
            }
            var focusId = user.Id;

            // check to see if current user has completed a survey
            var checkFocusCompleted = await _context.Answers
             .Where(a => a.Question.SurveyId == id && a.ResponderId == focusId)
             .ToListAsync();
            if (checkFocusCompleted.Count == 0)
            {
                return RedirectToAction("LoggedIn", "Surveys");
            }

            var surveyDB = await _context.Surveys.FirstOrDefaultAsync(m => m.SurveyId == id);

            var answersForResponders = await _context.Answers
             .Where(a => a.Question.SurveyId == id && a.FocusId == focusId && a.ResponderId != focusId)
             .GroupBy(a => a.ResponderId)
             .ToListAsync();

            var numOfResponders = answersForResponders.Count();
            if (numOfResponders == 0)
            {
                return RedirectToAction("LoggedIn", "Surveys");
            }

            var answerAll = await _context.Answers
            .Where(a => a.Question.SurveyId == id && a.FocusId == focusId)
            .GroupBy(a => a.Question)
            .Select(gQ => new
            {
                Question = gQ.Key,
                FocusResponseAnswer = gQ.FirstOrDefault(a => a.ResponderId == focusId),
                ResponseSumAll = gQ.Sum(x => x.Response ?? 0)
            }).ToListAsync();

            var vmitem = new List<GroupedAnswers>();

            foreach (var aa in answerAll)
            {
                int? questionAverage = (aa.ResponseSumAll - aa.FocusResponseAnswer.Response) / numOfResponders;

                GroupedAnswers pizza = new GroupedAnswers();

                pizza.QuestionId = aa.Question.QuestionId;
                pizza.QuestionText = aa.Question.QuestionText;
                pizza.FocusResponse = aa.FocusResponseAnswer?.Response ?? 0;
                pizza.ResponderCount = numOfResponders;
                pizza.ResponseSum = aa.ResponseSumAll;
                pizza.QuestionAverage = questionAverage ?? 0;


                vmitem.Add(pizza);
            }

            var viewModel = new ResultsViewModel();
            viewModel.SurveyName = surveyDB.SurveyName;
            viewModel.Description = surveyDB.Description;
            viewModel.ResponderCount = numOfResponders;
            viewModel.GroupedAnswers = vmitem;

            return View(viewModel);
        }




        // ************************************************************************  COMPLETE
        public async Task<IActionResult> Complete(int? id)
        {
            var model = new AnswerCreateViewModel();

            var surveyDB = await _context.Surveys.FirstOrDefaultAsync(m => m.SurveyId == id);

            var questionDB = await _context.Questions.Where(q => q.SurveyId == id).ToListAsync();

            // Get current user to assign ResponderUserId in VM
            var user = await GetCurrentUserAsync();

            // Get ALL Users to create a Select List to choose FocusUser
            var userDB = _context.Users;

            List<SelectListItem> recipientList = new List<SelectListItem>();

            recipientList.Insert(0, new SelectListItem { Text = "REQUIRED!! Select a person to Receive Feedback", Value = "" });

            foreach (var focusU in userDB)
            {
                SelectListItem li = new SelectListItem
                {
                    Value = focusU.Id,
                    Text = focusU.FullName
                };
                recipientList.Add(li);
            }

            if (model == null)
            {
                return NotFound();
            }

            var currentDateTime = DateTime.Now;

            var vmitem = new List<AnswerQuestionViewModel>();

            foreach (var question in questionDB)
            {
                AnswerQuestionViewModel taco = new AnswerQuestionViewModel();
                {
                    taco.QuestionId = question.QuestionId;
                    taco.QuestionText = question.QuestionText;
                    taco.Response = null;
                };
                vmitem.Add(taco);
            }

            var viewModel = new AnswerCreateViewModel();
            viewModel.SurveyId = surveyDB.SurveyId;
            viewModel.SurveyName = surveyDB.SurveyName;
            viewModel.Description = surveyDB.Description;
            viewModel.Instructions = surveyDB.Instructions;
            viewModel.AnswerQuestionViewModels = vmitem;
            viewModel.ResponderUserId = user.Id;
            viewModel.ResponderUserName = user.FullName;
            viewModel.Recipients = recipientList;
            viewModel.ResponseDate = currentDateTime;

            ViewData["Recipients"] = new SelectList(_context.ApplicationUsers, "FocusUserId", "FullName");

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Complete(int id, AnswerCreateViewModel viewModel)
        {
            var user = await GetCurrentUserAsync();
            var currentDateTimeTwo = DateTime.Now;
            var userDB = _context.Users;
            var surveyDB = await _context.Surveys.FirstOrDefaultAsync(m => m.SurveyId == id);
            var questionDB = await _context.Questions.Where(q => q.SurveyId == id).ToListAsync();

            //var aqVM = viewModel.AnswerQuestionViewModels;

            List<SelectListItem> recipientList = new List<SelectListItem>();

            recipientList.Insert(0, new SelectListItem { Text = "REQUIRED!! Select a person to Receive Feedback", Value = "" });

            foreach (var focusU in userDB)
            {
                SelectListItem li = new SelectListItem
                {
                    Value = focusU.Id,
                    Text = focusU.FullName
                };
                recipientList.Add(li);
            }

            var nullResponse = false;

            var vmitem = new List<AnswerQuestionViewModel>();

            foreach (var aqVM in viewModel.AnswerQuestionViewModels)
            {
                var oldResponse = aqVM.Response;
                var oldQuestionId = aqVM.QuestionId;

                Question oldQuestion = await _context.Questions.FindAsync(oldQuestionId);

                AnswerQuestionViewModel taco = new AnswerQuestionViewModel();
                {
                    taco.QuestionId = oldQuestionId;
                    taco.QuestionText = oldQuestion.QuestionText;
                    taco.Response = oldResponse;
                };
                vmitem.Add(taco);

                if (taco.Response == null)
                {
                    nullResponse = true;
                }

                //Answer duplicate = await _context.Answers
                //    .FirstOrDefaultAsync(a => a.FocusId == viewModel.FocusUserId
                //    && viewModel.ResponderUserId == user.Id && taco.QuestionId == a.QuestionId);
                //if (duplicate != null)
                //{
                //    return RedirectToAction("LoggedIn", "Surveys");
                //}
            }

            viewModel.AnswerQuestionViewModels = vmitem;
            viewModel.SurveyId = surveyDB.SurveyId;
            viewModel.SurveyName = surveyDB.SurveyName;
            viewModel.Description = surveyDB.Description;
            viewModel.Instructions = surveyDB.Instructions;
            viewModel.ResponderUserId = user.Id;
            viewModel.ResponderUserName = user.FullName;
            viewModel.ResponseDate = DateTime.Now;
            viewModel.Recipients = recipientList;
            ViewData["Recipients"] = new SelectList(_context.ApplicationUsers, "FocusUserId", "FullName");

            if (nullResponse == true || viewModel.FocusUserId == null)
            {
                return View(viewModel);
            }

            for (int i = 0; i < viewModel.AnswerQuestionViewModels.Count; i++)
            {
                Answer newAnswer = new Answer
                {
                    ResponderId = user.Id,
                    FocusId = viewModel.FocusUserId,
                    QuestionId = viewModel.AnswerQuestionViewModels[i].QuestionId,
                    Response = viewModel.AnswerQuestionViewModels[i].Response,
                    ResponseDate = viewModel.ResponseDate
                };
                _context.Add(newAnswer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("LoggedIn", "Surveys");
        }


        //--------------------------------------------------------------------------------------------- CREATE
        public async Task<IActionResult> Create(int? id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return NotFound();
            }

            var surveyQuestionListViewModel = await _context.Surveys.FindAsync(id);

            surveyQuestionListViewModel = await _context.Surveys
                        .Include(i => i.SurveyAssignments).ThenInclude(i => i.Question)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(i => i.SurveyId == id);


            if (surveyQuestionListViewModel == null)
            {
                return NotFound();
            }
            var viewModel = new SurveyQuestionsListViewModel();
            viewModel.SurveyId = surveyQuestionListViewModel.SurveyId;
            viewModel.SurveyName = surveyQuestionListViewModel.SurveyName;
            viewModel.Description = surveyQuestionListViewModel.Description;
            viewModel.Instructions = surveyQuestionListViewModel.Instructions;

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("AnswerId,Response,QuestionId,FocusId,ResponderId")] Answer answer)
        {
            ModelState.Remove("User");
            ModelState.Remove("ResponderId");
            var user = await GetCurrentUserAsync();
            answer.ResponderId = user.UserId;

            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("CompleteSurvey", new { id = answer.QuestionId });
            }

            return View(answer);
        }


        // ************************************************************************ DETAILS
        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }


        // ********************************************************************************  EDIT
        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText", answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerId,Response,ResponderId,QuestionId,FocusId")] Answer answer)
        {
            if (id != answer.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.AnswerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}
