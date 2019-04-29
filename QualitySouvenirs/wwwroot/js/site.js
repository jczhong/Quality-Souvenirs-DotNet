// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#SortSelector").on("change", function () {
        var url = location.href;
        var value = url.match(/sort=(.*)/);
        if (value != null) {
            url = url.replace(value[1], this.value);
        } else {
            if (location.search != "") {
                url = url + "&sort=" + this.value;
            } else {
                url = url + "?sort=" + this.value;
            }
        }
        if ($(this).attr("search") != null) {
            if (url.match(/search=/) == null) {
                var search = $(this).attr("search");
                if (location.search != "") {
                    url = url + "&search=" + search;
                } else {
                    url = url + "?search=" + search;
                }
            }
        }
        location.href = url;
    });
})