$(document).ready(function () {

    // Refresh operations dropdown
    $('#fieldFilters').change(function (e) {
        e.preventDefault();
        var field = { propertyName: $('#fieldFilters').val() };
        $.ajax({
            url: urlAction,
            type: 'POST',
            data: field,
            success: function (data) {
                $('#OperatorsDropDown').html(data);
            }
        });
    });

    $('#resetFilterAction').click(function (e) {
        $('#SearchAll').val('');
        if ($('#SearchByParameter').is('select')) {
            $('#SearchByParameter option:selected').val('');
        } else {
            $('#SearchByParameter').val('');
        }

        return true;
    });
});

function tableOrdering(a, b) {
    var field = { propertyName: a,
        direction: b
    };
    $.ajax({
        url: urlSortAction,
        type: 'POST',
        data: field,
        success: function (data) {
            $('#table-list').replaceWith(data);
        }
    });
}

// Country Prefix ------------------------------------------

function CountryChangeOperation() {
    var field = { iso2: $("#Country").val() };
    $.ajax({
        url: urlCountryPrefix,
        type: 'POST',
        data: field,
        success: function (data) {
            $('#Prefix').val(data);
        }
    });
};

// Pagination ------------------------------------------
function limitPagination(a) {
    ShowPaginationLoadingBar();
    var field = { limit: a };
    $.ajax({
        url: urlLimitAction,
        type: 'POST',
        data: field,
        success: function (data) {
            $('#table-list').replaceWith(data);
            $.ajax({
                url: urlPaginationAction,
                type: 'POST',
                data: field,
                success: function (data) {
                    $('#pagination').replaceWith(data);
                    HidePaginationLoadingBar();
                }
            });
        }
    });
}

function HidePaginationLoadingBar() {
    var loading = document.getElementById("miniLoading");
    if (loading != null) loading.style.display = 'none';
}
function ShowPaginationLoadingBar() {
    var loading = document.getElementById("miniLoading");
    if (loading != null) loading.style.display = 'block';
}