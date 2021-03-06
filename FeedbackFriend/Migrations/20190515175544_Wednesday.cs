﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackFriend.Migrations
{
    public partial class Wednesday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupedAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionText = table.Column<string>(nullable: true),
                    FocusResponse = table.Column<int>(nullable: false),
                    ResponseSum = table.Column<int>(nullable: false),
                    QuestionAverage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupedAnswers", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    SurveyName = table.Column<string>(maxLength: 55, nullable: false),
                    Instructions = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Assigned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_Surveys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupedQuestions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupedSurveyId = table.Column<int>(nullable: false),
                    GroupedSurveyName = table.Column<string>(nullable: true),
                    GroupedSurveyDescription = table.Column<string>(nullable: true),
                    GroupedSurveyInstructions = table.Column<string>(nullable: true),
                    GroupedQuestionId = table.Column<int>(nullable: false),
                    GroupedQuestionText = table.Column<string>(nullable: true),
                    GroupedQuestionCount = table.Column<int>(nullable: false),
                    GroupedUserId = table.Column<string>(nullable: true),
                    GroupedFirstName = table.Column<string>(nullable: true),
                    GroupedLastName = table.Column<string>(nullable: true),
                    AnswerId = table.Column<int>(nullable: true),
                    SurveyAssignmentId = table.Column<int>(nullable: true),
                    SurveysViewModelSurveyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupedQuestions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionText = table.Column<string>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    GroupedQuestionsID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_GroupedQuestions_GroupedQuestionsID",
                        column: x => x.GroupedQuestionsID,
                        principalTable: "GroupedQuestions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Response = table.Column<int>(nullable: false),
                    ResponderId = table.Column<string>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    FocusId = table.Column<string>(nullable: false),
                    ResponseDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAssignment",
                columns: table => new
                {
                    SurveyAssignmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SurveyId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FocusId = table.Column<string>(nullable: true),
                    Assigned = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAssignment", x => x.SurveyAssignmentId);
                    table.ForeignKey(
                        name: "FK_SurveyAssignment_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyAssignment_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyAssignment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveysViewModel",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    SurveyId1 = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveysViewModel", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_SurveysViewModel_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveysViewModel_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveysViewModel_Surveys_SurveyId1",
                        column: x => x.SurveyId1,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveysViewModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[] { "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5", 0, "f248e85b-2fc6-48b2-861f-d51bc57e8f8e", "admin@admin.com", true, "Colleen", "Woolsey", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEPOxEBA+vhfaC9I334J7dxXC5WzJzcvkvb0J9mF8Tvba8siXmAWQazZJBTuIcdMePw==", null, false, "3d59e587-1ed4-41ce-87b5-b3996b52c51e", false, null, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "SurveyId", "Assigned", "Description", "Instructions", "SurveyName", "UserId" },
                values: new object[,]
                {
                    { 1, false, "The primary objective of this survey is to collect feedback relative to a person's capacity for walking in another's shoes and how others experience their balance of analysis and sympathy.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree. Consider your experience of this individual relative to the way they balance analysis and sympathy and relative to your experience of their capacity for walking in another's shoes.", "Empathy", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 2, false, "The primary objective of this survey is twofold. 1. To collect feedback relative to a persons' capacity for passive hearing vs active listening. 2. To asses their attunement to the reality that it's not about what we tell people, but what they hear.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Listening vs hearing", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 3, false, "The primary objective of this survey is to assess flexibility and responsiveness in communication.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Just stop talking already", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 4, false, "The primary objective of this survey is to assess capacity for navigating emotional safety needs. How did this person balance the need to avoid pain and potential loss of what they value, danger and insecurity with the objective they were committed to?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Presentation Feedback", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 5, false, "The primary objective of this survey is to assess the balance between approaching problems aggressively vs reflectively. How much does the need to gain control of one's time factor in problem solving?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Problem Solving", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 6, false, "What is this person's style of influence? Primarily feeling, or fact? Can they move flexibly between them when it's called for? How much does the need to gain approval factor in their style of influence?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Influence", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 7, false, "The primary objective of this survey is to assess the balance between necessary stability and unnecessary resistance to change - Does this person prefer the certainty of misery or the misery of uncertainty?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Change", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 8, false, "The primary objective of this survey is to assess caution vs spontaneity in the quest for excellence. How does this person live in the time warp between carefully weighing options and possibly missing opportunities?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Decision Making", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 9, false, "The primary objective of this survey is to assess capacity for creating constructive feedback opportunities.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Feedback - Giving it", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 10, false, "The primary objective of this survey is to assess capacity for optimizing the receipt of feedback.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Feedback - Receiving it", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" },
                    { 11, false, "The primary objective of this survey is to assess capacity for optimizing feedback opportunities.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "A Short Sample Survey", "bef35377-9c9f-4349-8ffa-50e7c3e3c6f5" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "GroupedQuestionsID", "QuestionText", "SurveyId" },
                values: new object[,]
                {
                    { 1, null, "In your experience, is this person attentive to the schedules of others?", 1 },
                    { 91, null, "In your experience, does this person become more intense and reckless under stress?", 7 },
                    { 90, null, "In your experience, does this person become slow-paced and inflexible under stress?", 7 },
                    { 89, null, "To what degree do you experience them as flexible?", 7 },
                    { 88, null, "To what degree do you experience them as spontaneous?", 7 },
                    { 87, null, "To what degree do you experience them as energetic?", 7 },
                    { 86, null, "To what degree do you experience this person as methodical?", 7 },
                    { 85, null, "To what degree do you experience this person as a team-player?", 7 },
                    { 84, null, "How likely is this person to say, 'Let's try something new'?", 7 },
                    { 83, null, "How likely is this person to say, 'Let's keep things the way they are'?", 7 },
                    { 82, null, "To what degree do you experience this person as inspiring?", 6 },
                    { 81, null, "In your experience of them, does this person become a poor listener in the midst of conflict?", 6 },
                    { 80, null, "In your experience of them, does this person become skeptical and uncommunicative in the midst of conflict?", 6 },
                    { 79, null, "In your experience of them, does this person become pessimistic and introspective when under stress?", 6 },
                    { 78, null, "In your experience of them, does this person become impulsive and unrealistic when under stress?", 6 },
                    { 77, null, "To what extent do you experience this person as friendly and outgoing?", 6 },
                    { 76, null, "To what extent do you experience this person as calm and introspective?", 6 },
                    { 75, null, "How likely is this person to say, 'Let's stop and look at all the evidence'?", 6 },
                    { 74, null, "How likely is this person to say, 'Trust me, it will work great'?", 6 },
                    { 73, null, "To what extent do you experience this person as an adept negotiator?", 6 },
                    { 72, null, "To what extent do you experience this person as optimistic and enthusiastic?", 6 },
                    { 71, null, "To what extent do you experience this person as unreliable?", 6 },
                    { 70, null, "To what extent do you experience this person as inattentive - Do they miss what others value?", 6 },
                    { 69, null, "Do you experience this person as more objective than subjective?", 5 },
                    { 68, null, "In your experience, how likely is this person to say, 'We could solve this problem if you'd do what I say'?", 5 },
                    { 67, null, "In your experience, does this person miss opportunities?", 5 },
                    { 66, null, "In your experience, is this person adept at gathering information?", 5 },
                    { 65, null, "Do you experience this person as valuing function over relationship - achieving goals over valuing people?", 5 },
                    { 92, null, "In your experience, does this person tend toward sullen and stubborn in the midst of conflict?", 7 },
                    { 64, null, "Do you experience this person as indecisive?", 5 },
                    { 93, null, "In your experience, does this person become distracted and impulsive in the midst of conflict?", 7 },
                    { 95, null, "How likely is this person to say, 'Let's go for it'?", 8 },
                    { 122, null, "How likely are you to thank the person giving you feedback?", 11 },
                    { 121, null, "If someone said hard to hear words that caused you to change would you experience that as good?", 11 },
                    { 120, null, "How likely are you to dismiss feedback?", 10 },
                    { 119, null, "How likely are you to thank the person giving you feedback?", 10 },
                    { 118, null, "How likely are you to receive feedback with your body position open and hands at side?", 10 },
                    { 117, null, "How likely are you to receive feedback with your arms crossed saying, Go ahead ... Make my day?", 10 },
                    { 116, null, "How likely are you to adopt the cover my a-- stance when receiving feedback?", 10 },
                    { 115, null, "How likely are you to adopt the fig-leaf stance when receiving feedback - Hands protectively in front", 10 },
                    { 114, null, "If someone said hard to hear words that caused you to change would you experience that as good?", 10 },
                    { 113, null, "To what degree do you experience feedback as informative rather than good or bad?", 10 },
                    { 112, null, "To what degree do you experience yourself as censoring or holding back?", 9 },
                    { 111, null, "Do you see feedback as informative rather than positive or negative?", 9 },
                    { 110, null, "Is your feedback metaphoric and open to interpretation?", 9 },
                    { 109, null, "Does your feedback sound like advice - 'You should ... '?", 9 },
                    { 108, null, "Does your feedback sound like a judgement - 'You are ... '?", 9 },
                    { 107, null, "How likely are you to overcome the desire to know and be right before you give feedback?", 9 },
                    { 106, null, "How likely are you to overcome the assumption that you won't be heard?", 9 },
                    { 105, null, "In your experience, does this person become more reckless and overconfident when in the midst of conflict?", 8 },
                    { 104, null, "In your experience, does this person become more indecisive and unyielding in the midst of conflict?", 8 },
                    { 103, null, "In your experience, does this person become more controversial and insensitive when under stress?", 8 },
                    { 102, null, "In your experience, does this person become more exacting and perfectionistic when under stress?", 8 },
                    { 101, null, "To what degree do you experience this person as independent?", 8 },
                    { 100, null, "To what degree do you experience this person as accurate?", 8 },
                    { 99, null, "To what degree do you experience this person as decisive?", 8 },
                    { 98, null, "To what degree do you experience this person as having high standards?", 8 },
                    { 97, null, "To what degree do you experience this person as bold?", 8 },
                    { 96, null, "To what degree do you experience this person as conscientious?", 8 },
                    { 94, null, "How likely is this person to say, 'I'm not sure yet'?", 8 },
                    { 63, null, "Do you experience this person as apt to waste time and resources?", 5 },
                    { 62, null, "Do you experience this person as direct?", 5 },
                    { 61, null, "In your experience, does this person listen as slowly as they speak?", 5 },
                    { 28, null, "In your experience, does this person stop talking when the focus of the group shifts from the topic at hand?", 3 },
                    { 27, null, "In your experience, what is this person's capacity to create a refuge of silence when others are being unreasonable or irrational?", 3 },
                    { 26, null, "In your experience, is this person likely to stop talking in order to negotiate time to compose a thoughtful response?", 3 },
                    { 25, null, "In your experience, how much attunement does this person show to others' receptivity?", 3 },
                    { 24, null, "In your experience, how likely is this person to pursue the same conversation again and again?", 3 },
                    { 23, null, "In your experience, does this person stop talking when they're becoming repetitive?", 3 },
                    { 22, null, "In your experience, does this person stop talking after realizing they're interrupting?", 3 },
                    { 21, null, "In your experience, what is this person's capacity for productive silence?", 2 },
                    { 20, null, "In your experience, what percentage of this person's time was spent listening? (Where 1 = 10% and 10 = 100%)", 2 },
                    { 19, null, "In your experience, did this person take time to understand and validate other points of view before offering solutions?", 2 },
                    { 18, null, "In your experience, was this person attentive to what wasn't being said?", 2 },
                    { 17, null, "In your experience, did this person show appreciation for the strengths/pros of others' points of view?", 2 },
                    { 16, null, "In your experience, did this person validate the concerns of others?", 2 },
                    { 15, null, "In your experience, did this person acknowledge the concerns of others?", 2 },
                    { 14, null, "In your experience, was this person easily distracted?", 2 },
                    { 13, null, "In your experience, how attentively did this person listen?", 2 },
                    { 12, null, "In your experience, what is their capacity for seeing differing opinions as complimentary rather than adversarial?", 1 },
                    { 11, null, "In your experience, is this person aware of, and responsive to the level of team morale?", 1 },
                    { 10, null, "In your experience, how responsive is this person to decreases in momentum during meetings?", 1 },
                    { 9, null, "In your experience, how insistent is this person that others adapt to their problem solving style?", 1 },
                    { 8, null, "In your experience, how insistent is this person that others adapt to their communication style?", 1 },
                    { 7, null, "In your experience, how attuned is this person to the communication styles of others?", 1 },
                    { 6, null, "In your experience, how attuned is this person to the values of others?", 1 },
                    { 5, null, "In your experience, how attuned is this person to the likely stress points of their co-workers?", 1 },
                    { 4, null, "In your experience, how informed is this person about the responsibilities and job scope of others?", 1 },
                    { 3, null, "In your experience, what is this person's level of focus on measuring appreciable progress toward company objectives?", 1 },
                    { 2, null, "In your experience, what is this person's level of focus on company objectives?", 1 },
                    { 29, null, "In your experience, does this person have a tendency to offer unsolicited advice?", 3 },
                    { 30, null, "In your experience, does this person have a tendency to offer unsolicited critique?", 3 },
                    { 31, null, "In your experience, how likely is this person to substitute conversation for action - suffer from the paralysis of analysis?", 3 },
                    { 32, null, "In your experience, how likely is this person to agree with and act on the phrase, 'Delay is the deadliest form of denial'?", 3 },
                    { 60, null, "In your experience, does this person display irritation when interrupted?", 5 },
                    { 59, null, "In your experience, does this person become anxious, slow, or withdrawn in the midst of conflict?", 5 },
                    { 58, null, "In your experience, does this person become intimidating and confrontational in the midst of conflict?", 5 },
                    { 57, null, "How likely are you to describe this person as considerate, self-controlled, patient and cooperative?", 5 },
                    { 56, null, "How likely are you to describe this person as a self-starter, bold, determined and tenacious?", 5 },
                    { 55, null, "In your experience, how likely is this person to say, 'Let's give it some time'?", 5 },
                    { 54, null, "In your experience, how likely is this person to say, 'Let's do it now'?", 5 },
                    { 53, null, "In your experience, to what degree were they able to recognize and affirm what others valued?", 4 },
                    { 52, null, "In your experience, how flexible was this person in reframing difficult interactions?", 4 },
                    { 51, null, "In your experience, how much did this person seek to understand others before being understood?", 4 },
                    { 50, null, "In your experience, what was this person's level of defensiveness relative to questions?", 4 },
                    { 49, null, "In your experience, did this person fidget and/or have other distracting gestures?", 4 },
                    { 48, null, "In your experience, was this person's posture open, relaxed and receptive?", 4 },
                    { 123, null, "When giving Feedback, to what degree do you experience yourself as censoring or holding back?", 11 },
                    { 47, null, "In your experience, did this person maintain good eye contact?", 4 },
                    { 45, null, "In your opinion, was there mastery of the subject matter?", 4 },
                    { 44, null, "In your opinion, was preparation for this presentation in evidence?", 4 },
                    { 43, null, "In your opinion, was the pace of the presentation hurried?", 4 },
                    { 42, null, "In your opinion, did the presentation wander off course?", 4 },
                    { 41, null, "In your experience, did this person deal well with distractions?", 4 },
                    { 40, null, "In your opinion, did the presentation meet its stated objectives?", 4 },
                    { 39, null, "In your experience, was this person dismissive of other's concerns?", 4 },
                    { 38, null, "In your experience, did this person maintain a good balance between talking and listening?", 4 },
                    { 37, null, "How positively did you experience this person's tone of voice?", 4 },
                    { 36, null, "How positively did you experience this person's nonverbal communication?", 4 },
                    { 35, null, "In your experience, how successful was this person in communicating expectations for action/response?", 4 },
                    { 34, null, "In your experience, how successful was this person in communicating context?", 4 },
                    { 33, null, "In your opinion, how informative was this presentation?", 4 },
                    { 46, null, "In your opinion, was the material organized?", 4 },
                    { 124, null, "How likely are you to overcome the assumption that you won't be heard?", 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupedQuestions_AnswerId",
                table: "GroupedQuestions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupedQuestions_SurveyAssignmentId",
                table: "GroupedQuestions",
                column: "SurveyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupedQuestions_SurveysViewModelSurveyId",
                table: "GroupedQuestions",
                column: "SurveysViewModelSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_GroupedQuestionsID",
                table: "Questions",
                column: "GroupedQuestionsID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAssignment_QuestionId",
                table: "SurveyAssignment",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAssignment_SurveyId",
                table: "SurveyAssignment",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAssignment_UserId",
                table: "SurveyAssignment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveysViewModel_AnswerId",
                table: "SurveysViewModel",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveysViewModel_QuestionId",
                table: "SurveysViewModel",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveysViewModel_SurveyId1",
                table: "SurveysViewModel",
                column: "SurveyId1");

            migrationBuilder.CreateIndex(
                name: "IX_SurveysViewModel_UserId",
                table: "SurveysViewModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupedQuestions_Answers_AnswerId",
                table: "GroupedQuestions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupedQuestions_SurveyAssignment_SurveyAssignmentId",
                table: "GroupedQuestions",
                column: "SurveyAssignmentId",
                principalTable: "SurveyAssignment",
                principalColumn: "SurveyAssignmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupedQuestions_SurveysViewModel_SurveysViewModelSurveyId",
                table: "GroupedQuestions",
                column: "SurveysViewModelSurveyId",
                principalTable: "SurveysViewModel",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAssignment_Questions_QuestionId",
                table: "SurveyAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveysViewModel_Questions_QuestionId",
                table: "SurveysViewModel");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GroupedAnswers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "GroupedQuestions");

            migrationBuilder.DropTable(
                name: "SurveyAssignment");

            migrationBuilder.DropTable(
                name: "SurveysViewModel");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
