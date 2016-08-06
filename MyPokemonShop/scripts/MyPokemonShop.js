$(document).ready(function () {
    $('form  input[type=text]').on("input", function () {
        var empty = false;
        $('form  input[type=text]').each(function () {
            if ($(this).val() == '') {
                empty = true;
            }
        });

        if (empty) {
            $('#MakeOrder').prop('disabled', true);
        } else {
            $('#MakeOrder').prop('disabled', false);
        }
    });

});


