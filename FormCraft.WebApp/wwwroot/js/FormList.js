
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
    }
    );
}

let timer;
function addTimer() {

    clearTimeout(timer);
    timer = setTimeout(SearchForm, 300);
}

const onChangeEvents = () => {

    let labelInput = document.querySelector('#labelInput');

    labelInput.addEventListener('input', addTimer
    );

    document.querySelectorAll('.form-check-input')
        .forEach(c => c.addEventListener('change', addTimer));
}



$(function () {
    //Delete
    $(".delete").on('click', e => {
        var formId = $(this).data("form-id");

        if (confirm('Are you sure ?')) {
            $.post("/Form/Delete", { id: formId },
                data => {
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

    //Search
    $("#searchBtn").on('click', e => SearchForm(e));

    onChangeEvents();
});

$(function () {
    $(".duplicate").each((i, b) => {

        var formId = $(b).data("form-id");
        $(b).on('click', e => {
            console.log('enter in duplicate method')
            $.post("/Form/Duplicate", { id: formId },
                data => {
                    $('#list-container').append(data);
                    alert('Duplication succeed');
                }
            )
            e.preventDefault();
        })
    });

    //document.querySelectorAll('.duplicate')
    //    .forEach(b => {
    //        var formId = $(b).data("form-id");

    //})
});