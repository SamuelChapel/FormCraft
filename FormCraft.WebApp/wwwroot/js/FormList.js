

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

    $("#searchBtn").on('click', e => {

        //let formList = $("#search-form-form");

        let label = $('#labelInput').val();
        let userId = "";
        let order = $('#orderInput').val();

        //let isTypeSurveyChecked = $('#formTypeInput-Survey').is(":checked");
        //let isTypeCommentChecked = $('#formTypeInput-Comment').is(":checked");
        //let isTypeEvaluationChecked = $('#formTypeInput-Evaluation').is(":checked");

        //let isInProgressChecked = $('#statusInput-InProgress').is(":checked");
        //let isValidatedChecked = $('#statusInput-Validated').is(":checked");
        //let isClosedChecked = $('#statusInput-Closed').is(":checked");

        let formTypeValues = $('input[name="IsFormTypePicked"]:checked').map(function () {
            return $(this).val();
        }).get();

        let statusValues = $('input[name="IsStatusEnumPicked"]:checked').map(function () {
            return $(this).val();
        }).get();

        let request = {
            Label: label,
            CurrentUserId: userId,
            IsStatusEnumPicked: statusValues,
            IsFormTypePicked: formTypeValues,
            Order: parseInt(order)
        };

        console.log("request test", request);

        $.post("/Form/Search", request = request, data => {

            console.log("enter method");

            $('#list-container').empty();
            $('#list-container').html(data);
        }
        );

        e.preventDefault();
    });
});




