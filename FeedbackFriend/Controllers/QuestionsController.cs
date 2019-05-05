﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedbackFriend.Data;
using FeedbackFriend.Models;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FeedbackFriend.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // ******************************************************************************** INDEX
        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Questions.Include(q => q.Survey);
            return View(await applicationDbContext.ToListAsync());
        }


        // ******************************************************************************** DETAILS
        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.Survey)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // ******************************************************************************** CREATE
        // GET: Questions/Create

        //public async Task<IActionResult> Create(int? surveyId)
        //{
        //    var applicationDbContext = _context.Surveys;

        //    var SurveysViewModel = await _context.Surveys
        //        .Include(s => s.Questions)
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

        //    //if (SurveysViewModel == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    var model = new SurveysViewModel();

        //    model.GroupedQuestions = await (
        //        from s in _context.Surveys
        //        join q in _context.Questions
        //        on s.SurveyId equals q.SurveyId
        //        group new { s, q } by new { s.SurveyId, s.SurveyName, s.Description, s.Instructions } into grouped
        //        select new GroupedQuestions
        //        {
        //            SurveyDetailId = grouped.Key.SurveyId,
        //            SurveyDetailName = grouped.Key.SurveyName,
        //            SurveyDetailDescription = grouped.Key.Description,
        //            SurveyDetailInstructions = grouped.Key.Instructions,

        //            QuestionCount = grouped.Select(x => x.q.QuestionId).Count(),
        //            Questions = grouped.Select(x => x.q)
        //        }).ToListAsync();

        //    return View(model);
        //}
        // --------------------------------------------------
        //public async Task<IActionResult> Create(int? surveyId)
        //{
        //    var applicationDbContext = _context.Surveys;

        //    var surveysViewModel = await _context.Surveys
        //        .Include(s => s.Questions)
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

        //    if (surveysViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    var viewModel = new SurveysViewModel();
        //    viewModel.SurveyId = surveysViewModel.SurveyId;
        //    viewModel.SurveyName = surveysViewModel.SurveyName;
        //    viewModel.Description = surveysViewModel.Description;
        //    viewModel.Instructions = surveysViewModel.Instructions;

        //    return View(viewModel);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]


        //public async Task<IActionResult> Create([Bind("SurveyId,QuestionText")] SurveysViewModel surveysViewModel, int surveyId)
        //{
        //    var applicationDbContext = _context.Questions.Include(a => a.Survey);
        //    try
        //    {
        //        var SurveysViewModel = await _context.Questions
        //        .Include(q => q.Survey)
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

        //        //if (SurveysViewModel == null)
        //        //{
        //        //    return NotFound();
        //        //}              

        //        if (ModelState.IsValid)
        //        {
        //            var viewModel = new SurveysViewModel();
        //            {
        //                viewModel.SurveyId = surveyId;
        //                viewModel.QuestionText = surveysViewModel.QuestionText;
        //            };



        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        // PopulateDepartmentsDropDownList(question.DepartmentId);
        //        return View(surveysViewModel);
        //        //ViewData.Add("SurveyId", value: "surveyId");
        //        //     ViewData.Add("QuestionText", value: "Questiontext");
        //        //    await _context.SaveChangesAsync();
        //        //    return RedirectToAction(nameof(Create));

        //    }
        //    catch (DbUpdateException /* ex */)
        //    {
        //        //Log the error (uncomment ex variable name and write a log.
        //        ModelState.AddModelError("", "Unable to save changes. " +
        //            "Try again, and if the problem persists " +
        //            "see your system administrator.");
        //    }
        //    return View(surveysViewModel);
        //}

        // TEMPLATE: return RedirectToAction("{controller=Home}/{action=Index}/{id?}");
        // return RedirectToAction("Create", "Questions", "question.SurveyId");   



        // ****************************************************************************************** EDIT
        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyName", question.SurveyId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,SurveyId,QuestionText")] Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
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
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyName", question.SurveyId);
            return View(question);
        }


        // ****************************************************************************************** DELETE
        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Survey)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }




        //--------------------------------------------------------------------------------------------- CREATE

        //GET: Surveys with Questions/Create ---- THIS CODE WORKS IN THE get!!!!!!!!!

        public async Task<IActionResult> Create(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            var applicationDbContext = _context.Surveys;

            var surveysViewModel = await _context.Surveys
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

            if (surveysViewModel == null)
            {
                return NotFound();
            }
            var viewModel = new SurveysViewModel();
            viewModel.SurveyId = surveysViewModel.SurveyId;
            viewModel.SurveyName = surveysViewModel.SurveyName;
            viewModel.Description = surveysViewModel.Description;
            viewModel.Instructions = surveysViewModel.Instructions;
            viewModel.IEnumQuestions = surveysViewModel.Questions;

            return View(viewModel);
        }

       

        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx  CREATE
        // GET: Survey to add questions to and List of questions already added/Create/5

        //public async Task<IActionResult> Create(int? surveyId)
        //{
        //    if (surveyId == null)
        //    {
        //        return NotFound();
        //    }

        //    var survey = await _context.Surveys
        //        .Include(i => i.QuestionAssignments).ThenInclude(i => i.Question)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);
        //    if (survey == null)
        //    {
        //        return NotFound();
        //    }
        //    PopulateAssignedQuestionData(survey);
        //    return View(survey);
        //}

        //private void PopulateAssignedQuestionData(Survey survey)
        //{
        //    var allQuestions = _context.Questions;
        //    var surveyQuestions = new HashSet<int>(survey.QuestionAssignments.Select(q => q.QuestionId));
        //    var viewModel = new List<SurveyQuestionsCREATEViewModel>();
        //    foreach (var question in allQuestions)
        //    {
        //        viewModel.Add(new SurveyQuestionsCREATEViewModel
        //        {
        //            QuestionId = question.QuestionId,
        //            QuestionText = question.QuestionText,
        //            Assigned = surveyQuestions.Contains(question.QuestionId)
        //        });
        //    }
        //    ViewData["Questions"] = viewModel;
        //}



        // POST: Add questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("SurveyId,QuestionText")] int? surveyId, SurveyQuestionsEDITViewModel surveyQuestionsEDITViewModel)
        {
            var survey = await _context.Surveys
                .Include(i => i.QuestionAssignments).ThenInclude(i => i.Question)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.SurveyId == surveyId);
            //if (survey == null)
            //{
            //    return NotFound();
            //}

            PopulateAssignedQuestionData(survey);
                        
            if (ModelState.IsValid)
            {
                Question question = new Question
                {
                    SurveyId = survey.SurveyId,
                    QuestionText = surveyQuestionsEDITViewModel.QuestionText
                };

                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surveyQuestionsEDITViewModel);

        }
        private void PopulateAssignedQuestionData(Survey survey)
        {
            var allQuestions = _context.Questions.Where(q => q.SurveyId == survey.SurveyId);
            var surveyQuestions = new HashSet<int>(survey.QuestionAssignments.Select(q => q.QuestionId));
            var viewModel = new List<SurveyQuestionsEDITViewModel>();
            foreach (var question in allQuestions)
            {
                viewModel.Add(new SurveyQuestionsEDITViewModel
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    Assigned = surveyQuestions.Contains(question.QuestionId)
                });
            }
            ViewData["Questions"] = viewModel;
        }

        //public async Task<IActionResult> Create(int surveyId)
        //{
        //    var questionToAdd = await _context.Questions
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

        //    if (await TryUpdateModelAsync<Question>(
        //        questionToAdd,
        //        "",
        //        i => i.QuestionText, i => i.SurveyId))
        //    {

        //        UpdateSurveyQuestions(selectedQuestions, surveyToAdd);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            Log the error(uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes.");
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    UpdateSurveyQuestions(selectedQuestions, surveyToAdd);
        //    PopulateAssignedQuestionData(surveyToAdd);
        //    return View(questionToAdd);
        //}

        //private void UpdateSurveyQuestions(string[] selectedQuestions, Survey surveyToAdd)
        //{
        //    if (selectedQuestions == null)
        //    {
        //        surveyToAdd.QuestionAssignments = new List<QuestionAssignment>();
        //        return;
        //    }

        //    var selectedQuestionsHS = new HashSet<string>(selectedQuestions);
        //    var surveyQuestions = new HashSet<int>
        //        (surveyToAdd.QuestionAssignments.Select(c => c.Question.QuestionId));
        //    foreach (var question in _context.Questions)
        //    {
        //        if (selectedQuestionsHS.Contains(question.QuestionId.ToString()))
        //        {
        //            if (!surveyQuestions.Contains(question.QuestionId))
        //            {
        //                surveyToAdd.QuestionAssignments.Add(new QuestionAssignment { SurveyId = surveyToAdd.SurveyId, QuestionId = question.QuestionId });
        //            }
        //        }
        //        else
        //        {

        //            if (surveyQuestions.Contains(question.QuestionId))
        //            {
        //                QuestionAssignment questionToRemove = surveyToAdd.QuestionAssignments.FirstOrDefault(i => i.QuestionId == question.QuestionId);
        //                _context.Remove(questionToRemove);
        //            }
        //        }
        //    }
        //}

    }
}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Create(SurveysViewModel surveysViewModel)
//{
//    try
//    {
//        using (SqlConnection conn = Connection)
//        {
//            conn.Open();
//            using (SqlCommand cmd = conn.CreateCommand())
//            {
//                cmd.CommandText = @"INSERT INTO Question(QuestionText, SurveyId) 
//                                    VALUES (@text, @surveyId)";
//                cmd.Parameters.Add(new SqlParameter("@name", surveysViewModel.QuestionText));
//                cmd.Parameters.Add(new SqlParameter("@surveyId", surveysViewModel.SurveyId));

//                cmd.ExecuteNonQuery();
//                return RedirectToAction(nameof(Index));
//            }
//        }
//    }
//    catch
//    {
//        return View(surveysViewModel);
//    }
//}