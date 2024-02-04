const questionsContainer = document.getElementById("question-list-container");

const UpdateQuestionLabel = e => {
    $.post('/Question/Update', {
        Id: e.getAttribute("data-id"),
        Label: e.value
    });
}

const UpdateQuestionType = e => {
    $.post('/Question/Update', {
        Id: e.getAttribute("data-id"),
        QuestionTypeId: e.value
    });
}

const UpdateAnswer = e => {
    $.post('/Answer/Update', {
        Id: e.getAttribute("data-id"),
        Label: e.value
    });
}

const RemoveQuestion = e => {
    $.post('/Question/Delete', {
        Id: e.getAttribute("data-id")
    },
        () => {
            e.parentElement.parentElement.remove();
        }
    );
}

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

const RemoveAnswer = e => {
    $.post('/Answer/Delete', {
        Id: e.getAttribute("data-id")
    },
        () => {
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