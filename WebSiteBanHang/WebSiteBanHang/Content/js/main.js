$(document).ready(function () {
    
    $("#TenKH").blur(function () {
        var loi = 0;
        if ($("#TenKH").val() == "") {
            $("#TB_TenKH").text("Xin nhập Tên khách hàng !");
            false;
        } else {          
            $("#TB_TenKH").text("");
        }
    });
    $('ul li a').click(function () {
        $('li a').removeClass("active");
        $(this).addClass("active");
    });
    (function () {

        $("#cart").on("click", function () {
            $(".shopping-cart").fadeToggle("fast");
        });

    })();
    

});