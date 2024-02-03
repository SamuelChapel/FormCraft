const questionsContainer = document.getElementById("question-list-container");

const AddAnswer = e => {
    console.log(e.getAttribute("data-question-id"), e.getAttribute("data-question-number"), e);

    $.post('/Answer/Create', {
        Label: 'Answer',
        QuestionId: e.getAttribute("data-question-id")
    },
        (answerResponse) => {
            console.log(answerResponse);
            $('#' + e.getAttribute("data-question-number") + 'question-answers').append(answerResponse);
        }
    );
};

const RemoveQuestion = e => {
    $.post('/Question/Delete', {
        Id: e.getAttribute("data-id")
    },
        response => {
            e.parentElement.parentElement.remove();
        }
    );
}

$(function () {
    $('#create-form').on("submit", e => {
        console.log(e.target.formTitle.value, e.target.formType.value);

        $.post('/Form/Create', {
            label: e.target.formTitle.value,
            formType: e.target.formType.value
        },
            (formResponse) => {
                console.log(formResponse);
            }
        );

        e.preventDefault();
    });

    $('#add-question-button').on('click', e => {
        $.post('/Question/Create', {
            Number: $('#question-list-container').length + 1,
            Label: 'Question Title',
            QuestionTypeId: 1,
            FormId: e.target.getAttribute("data-form-id")
        },
            (questionResponse) => {
                $('#question-list-container').append(questionResponse);
            }
        );

        e.preventDefault();
    });

    $('#add-answer-button').on('click', e => {
        console.log(e.target);
        console.log(e.target.getAttribute("data-question-id"), e.target.getAttribute("data-question-number"))
        $.post('/Answer/Create', {
            Label: 'Answer',
            QuestionId: e.target.getAttribute("data-question-id")
        },
            (answerResponse) => {

                $('#' + e.target.getAttribute("data-question-number") + 'question-list-container').append(answerResponse);
            }
        );

        e.preventDefault();
    });
});