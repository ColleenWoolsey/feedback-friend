﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class AnswerCreateViewModel
    {  
        
        public int SurveyId { get; set; }

        public string SurveyName { get; set; }

        public string Instructions { get; set; }

        public string Description { get; set; }

        public string FocusUserId { get; set; }
        public ApplicationUser FocusUser { get; set; }

        [Range(1, 10, ErrorMessage = "The value must be between 1 and 10")]
        public int? Response { get; set; }
                       
        public string QuestionText { get; set; }

        public List<AnswerQuestionViewModel> AnswerQuestionViewModels { get; set; }
    }
}