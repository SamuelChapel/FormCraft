const questionsContainer = document.getElementById("question-list-container");

const AddAnswer = e => {
    $.post('/Answer/Create', {
        Label: 'Answer',
        QuestionId: e.getAttribute("data-question-id")
    },
        (answerResponse) => {
            $('#' + e.getAttribute("data-question-number") + '-question-answers').append(answerResponse);
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

const RemoveAnswer = e => {
    $.post('/Answer/Delete', {
        Id: e.getAttribute("data-id")
    },
        response => {
            console.log(e.parentElement);
            e.parentElement.remove();
        }
    );
}

$(function () {
    $('#add-question-button').on('click', e => {
        $.post('/Question/Create', {
            Number: $('#question-list-container').children().length + 1,
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
});