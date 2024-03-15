// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showToaster(msg, type = "success") {

    //#region Toaster Options
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    //#endregion

    switch (type) {
        case "info":
            toastr.info(msg);
            break;
        case "warning":
            toastr.warning(msg);
            break;
        case "error":
            toastr.error(msg);
            break;
        default:
            toastr.success(msg);
            break;
    }

}
