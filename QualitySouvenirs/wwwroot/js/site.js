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

    $('button[id^=AddToCart-]').click(function () {
        var id = this.value;
        $.post("/ShoppingCart/Add",
            {
                id: id,
                quantity: 1
            },
            function (data, status) {
                if (status == "success") {
                    var count = $('#CartCount').text();
                    if (count == undefined) {
                        count = 0;
                    }
                    count = Number(count);
                    count += 1;
                    $("#CartCount").text(count);
                } else {
                    alert("Out of stock!");
                }
            });
    });

    $('button[id^=RemoveFromCart-]').click(function () {
        var id = this.value;
        $.post("/ShoppingCart/Remove",
            {
                id: id,
                quantity: 1
            },
            function (data, status) {
                if (status == "success") {
                    var count = $("#CartCount").text();
                    if (count == undefined) {
                        count = 0;
                    }
                    count = Number(count);
                    if (count > 0) {
                        count -= 1;
                        $("#CartCount").text(count);
                    }
                }
            });
    });
})