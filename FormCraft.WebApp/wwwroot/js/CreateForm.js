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
            UpdateCraftButtonStatus();
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

    e.preventDefault();
}

const UpdateCraftButtonStatus = () => {
    if ($('#question-list-container').children().length == 0) {
        $('#validate-form-button').prop('disabled', true);
    } else {
        $('#validate-form-button').prop('disabled', false);
    }
}

$(function () {
    $('#add-question-button').on('click', e => {
        let number = $('#question-list-container').children().length + 1;
        $.post('/Question/Create', {
            Number: number,
            Label: 'Question Title',
            QuestionTypeId: 1,
            FormId: e.target.getAttribute("data-form-id")
        },
            (questionResponse) => {
                $('#question-list-container').append(questionResponse);
                UpdateCraftButtonStatus();
                let question = $('#question-list-container').last();

                $.post('/Answer/Create', {
                    Label: 'Answer',
                    QuestionId: $('#' + number + '-question-title').data('id')
                },
                    (answerResponse) => {
                        $('#' + number + '-question-answers').append(answerResponse);
                    }
                );
            }
        );

        e.preventDefault();
    });
});