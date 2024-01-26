using FormCraft.Entities;



namespace FormCraft.Tests.Commons.Builders
{
    public class QuestionBuilder
    {
        private string _id = "testid";
        private int _number = 4;
        private string _label = "testlabel";
        private QuestionType? _questionType = new QuestionType() {Id = 3, Label = "testlabeltype"};
        private Form? _form = new Form() { Id = "idb" };
        private List<Answer> _answers = new List<Answer>();

        public static QuestionBuilder AQuestion => new();

        public QuestionBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public QuestionBuilder WithNumber(int number)
        {
            _number = number;
            return this;
        }

        public QuestionBuilder WithLabel(string label)
        {
            _label = label;
            return this;
        }

        public QuestionBuilder WithQuestionType(QuestionType questionType)
        {
            _questionType = questionType;
            return this;
        }

        public QuestionBuilder WithForm(Form form)
        {
            _form = form;
            return this;
        }

        public QuestionBuilder WithAnswer(Answer answer)
        {
            _answers.Add(answer);
            return this;
        }

        public Question Build()
        {
            var question = new Question
            {
                Id = _id,
                Number = _number,
                Label = _label,
                QuestionType = _questionType,
                Form = _form,
                Answers = _answers,
            };

            return question;
        }
    }
}
