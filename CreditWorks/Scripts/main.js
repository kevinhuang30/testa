
submitForm($('#vehicleForm'));
submitForm($('#categoryForm'));

$(document).ready(function () {
    $(".tip-sub").hide();
});

//valid Create Vehicle Information form
//Year of manufacture should be digits only and allow Weight for 2 decimal places

jQuery.validator.addMethod("dollarsscents", function (value, element) {
   return this.optional(element) || /^\d{0,10}(\.\d{0,2})?$/i.test(value);
}, "You must include two decimal places");

$('#vehicleForm').validate({
    rules: {
        YearOfManufacture: {
            required: true,
            minlength: 4,
            maxlength: 4,
            digits: true
        },
        WeightOfVehicle: {
            required: true,
            dollarsscents: true       
        }
    }
});

//submit Vehicle Information form
// show tips if successful then fade out.
function submitForm(frm) {
    frm.submit(function (e) {
        if (frm.valid()) {
            e.preventDefault();
            $.ajax({
                type: frm.attr('method'),
                url: frm.attr('action'),
                data: frm.serialize(),
                success: function (data) {
                    $(".tip-sub").show();
                    setTimeout(function () {
                        $('.tip-sub').fadeOut('slow');
                    }, 1800);
                    console.log('Submission was successful.');
                    location.reload();
                },
                error: function (data) {
                    console.log('An error occurred.', data);
                },
            });
        }
    });
}


//table sorter
$(function () {
    $("#myTable").tablesorter({
        sortList: [[0, 0], [1, 0]]
    })
})




