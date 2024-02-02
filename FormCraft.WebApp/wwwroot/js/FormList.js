

$(function () {
    $(".delete").on('click', function (e) {
        var formId = $(this).data("form-id");

        if (confirm('Are you sure ?')) {
            $.post("/Form/Delete", { id: formId },
                function (data) {
                    alert('Deleted');
                    $("#row-" + formId).remove();
                })
                .fail(function (error) {
                    alert('Error !');
                    console.error(error);
                });
        }
        e.preventDefault();
    });

    $("#searchBtn").on("click", e => {

        console.log("enter test");

        let formList = $("#search-form-form");
        console.log("form list : ", formList);

        let label = $('#labelInput').val();
        let userId = "";
        let order = $('#orderInput').val();
        let isTypeSurveyChecked = $('#formTypeInput-Survey').is(":checked");
        let isTypeCommentChecked = $('#formTypeInput-Comment').is(":checked");
        let isTypeEvaluationChecked = $('#formTypeInput-Evaluation').is(":checked");

        let isInProgressChecked = $('#statusInput-InProgress').is(":checked");
        let isValidatedChecked = $('#statusInput-Validated').is(":checked");
        let isClosedChecked = $('#statusInput-Closed').is(":checked");

        let formRequest = { Label: label, CurrentUserId: userId, IsStatusEnumPicked: [isInProgressChecked, isValidatedChecked, isClosedChecked], IsFormTypePicked: [isTypeSurveyChecked, isTypeCommentChecked, isTypeEvaluationChecked], Order: order };
        console.log("request test", formRequest);

        $.post("/Form/Search", { request: formRequest },
            function (data) {
                $('#list-container').empty();
                $('#list-container').html(data);
            }
        )
            .fail(function (error) {
                alert('Une erreur s\'est produite.');
                console.error(error);
            });
        e.preventDefault();
    });
});

