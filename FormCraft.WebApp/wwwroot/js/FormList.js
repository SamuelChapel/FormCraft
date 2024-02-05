
let timer;
function addTimer() {

    clearTimeout(timer);
    timer = setTimeout(SearchForm, 300);
}
const SearchForm = () => {
    let label = $('#labelInput').val();
    let userId = "";
    let order = $('#orderInput').val();
    let formTypeValues = $('input[name="IsFormTypePicked"]:checked').map(function () {
        return this.value;
    }).get();

    let statusValues = $('input[name="IsStatusEnumPicked"]:checked').map(function () {
        return this.value;
    }).get();


    let request = {
        Label: label,
        CurrentUserId: userId,
        IsStatusEnumPicked: statusValues,
        IsFormTypePicked: formTypeValues,
        Order: parseInt(order)
    };

    $.post("/Form/Search", request, data => {
        $('#list-container').empty();
        $('#list-container').html(data);
        duplicateFormEvent();
        deleteFormEvent();
    }
    );
}
const onChangeEvents = () => {

    let labelInput = document.querySelector('#labelInput');

    labelInput.addEventListener('input', addTimer
    );

    document.querySelectorAll('.form-check-input')
        .forEach(c => c.addEventListener('change', addTimer));
}

const deleteFormEvent = () => {
    $(".delete").each((i, b) => {

        var formId = $(b).data("form-id");
        $(b).on('click', e => {
            if (confirm('Are you sure ?')) {
                $.post("/Form/Delete", { id: formId },
                    data => {
                        $("#row-" + formId).remove();
                        alert('Deleted');
                    })
                    .fail(function (error) {
                        alert('Error !');
                        console.error(error);
                    });
            }
            e.preventDefault();
        });
    });
}

const duplicateFormEvent = () => {
    $(".duplicate").each((i, b) => {

        var formId = $(b).data("form-id");
        $(b).on('click', e => {
            $.post("/Form/Duplicate", { id: formId },
                data => {
                    $('#list-container').append(data);
                    alert('Duplication succeed');
                }
            )
            e.preventDefault();
        })
    });
}

$(function () {
    $("#searchBtn").on('click', e => SearchForm(e));

    //Delete
    deleteFormEvent();
    //Search
    onChangeEvents();
    //Duplicate
    duplicateFormEvent();
});

