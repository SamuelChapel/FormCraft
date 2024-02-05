
const SearchForm = () => {
    console.log('launcher de la mort qui tue')
    let label = $('#labelInput').val();
    let userId = "";
    let order = $('#orderInput').val();

    //let formTypeValues = $('input[name="IsFormTypePicked"]:checked').map(c => c.val()).get();
    let formTypeValues = $('input[name="IsFormTypePicked"]:checked').map(function () {
        return this.value;
    }).get();

    //let statusValues = $('input[name="IsStatusEnumPicked"]:checked').map(c => c.val()).get();
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
    console.log('enter timer' + timer)
}

const onChangeEvents = () => {

    let labelInput = document.querySelector('#labelInput');

    labelInput.addEventListener('input', addTimer
    );

    //$("#labelInput").on("blur", e => {
    //    clearTimeout(timer);
    //    timer = setTimeout(() => SearchForm(), 0);
    //    e.preventDefault();
    //    console.log('Enter change event');
    //});

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