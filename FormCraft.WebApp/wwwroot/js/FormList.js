$(function () {
    $(".delete").on('click', function (e) {
        e.preventDefault();

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
    });
});