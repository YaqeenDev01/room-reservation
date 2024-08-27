
$('#ProductSelect').change(function () {
    var ProductSelect = $('#ProductSelect').val();
            $.ajax({
                type: "POST",
                url: "/Products/GetProductById",
                data: { id: ProductSelect },
                dataType: "json",
                success: function (data) {
                    if (data.categoryId == 0) {
                        $('#ProductPrice').val("0");
                        $('#ProductWholesalePrice').val("0");
                    }
                    else {
                    $('#ProductPrice').val(data.productPrice);
                    $('#ProductWholesalePrice').val(data.productWholesalePrice);
                    }
                    var NetProfit = (parseFloat($('#ProductPrice').val()) * parseInt($('#Quantity').val())) - (parseFloat($('#ExtraSpending').val()) + (parseFloat($('#ProductWholesalePrice').val()) * parseInt($('#Quantity').val())));
                    var MarketerPrice = (parseFloat($('#MarketerPercentage').val()) / 100) * NetProfit;
                    $('#MarketerPrice').val(MarketerPrice);
                    NetProfit = NetProfit - MarketerPrice;
                    $('#NetProfit').val(NetProfit);
                },
                //End of AJAX Success function
                failure: function (data) { },
                //End of AJAX failure function
                error: function (data) {
                    alert(data.responseText);
                }
                //End of AJAX error function
            });

});
$('#MarketerSelect').change(function () {
    var MarketerSelect = $('#MarketerSelect').val();
    $.ajax({
        type: "POST",
        url: "/Marketer/GetMarketerById",
        data: { id: MarketerSelect },
        dataType: "json",
        success: function (data) {
            console.log(data);
            if (data.marketerId == 0) {
                $('#MarketerPercentage').val("0");
            }
            else {
                $('#MarketerPercentage').val(data.marketerPercentage);
            }
            var NetProfit = (parseFloat($('#ProductPrice').val()) * parseInt($('#Quantity').val())) - (parseFloat($('#ExtraSpending').val()) + (parseFloat($('#ProductWholesalePrice').val()) * parseInt($('#Quantity').val())));
            var MarketerPrice = (parseFloat($('#MarketerPercentage').val()) / 100) * NetProfit;
            $('#MarketerPrice').val(MarketerPrice);
            NetProfit = NetProfit - MarketerPrice;;
            $('#NetProfit').val(NetProfit);
        },
        //End of AJAX Success function
        failure: function (data) { },
        //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        }
        //End of AJAX error function
    });
});
$('#ExtraSpending').change(function () {
    var NetProfit = (parseFloat($('#ProductPrice').val()) * parseInt($('#Quantity').val())) - (parseFloat($('#ExtraSpending').val()) + (parseFloat($('#ProductWholesalePrice').val()) * parseInt($('#Quantity').val())));
    var MarketerPrice = (parseFloat($('#MarketerPercentage').val()) / 100) * NetProfit;
    $('#MarketerPrice').val(MarketerPrice);
    NetProfit = NetProfit - MarketerPrice;
    $('#NetProfit').val(NetProfit);
});
$('#Quantity').change(function () {
    var NetProfit = (parseFloat($('#ProductPrice').val()) * parseInt($('#Quantity').val())) - (parseFloat($('#ExtraSpending').val()) + (parseFloat($('#ProductWholesalePrice').val()) * parseInt($('#Quantity').val())));
    var MarketerPrice = (parseFloat($('#MarketerPercentage').val()) / 100) * NetProfit;
    $('#MarketerPrice').val(MarketerPrice);
    NetProfit = NetProfit - MarketerPrice;
    $('#NetProfit').val(NetProfit);
});