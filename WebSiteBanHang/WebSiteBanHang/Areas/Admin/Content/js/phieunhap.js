$("#btnAdd").click(function () {
    // Lấy id của tr cuối cùng thuộc thẻ table có class = tblChiTietPhieuNhap
    //Bước 4: Phương thức find là tìm đến thẻ nào đó: ở đây là thẻ tr (:last-child) là thẻ tr cuối cùng trong thẻ tblChiTietPhieuNhap
    var id_cuoi = $(".tblChiTietPhieuNhap").find("tr:last-child").attr("data-id");
    i = parseInt(id_cuoi) + 1;
    //Bước 1: Nội dung phía trong thẻ trAppend
    var tdnoidung = $(".trAppend").html();
    //Bước 2:Tạo 1 thẻ tr bao ngoài nội dung
    var trnoidung = "<tr class=\"trAppended\" data-id=\"" + i + "\">" + tdnoidung + "</tr>";
    ////Bước 3: Lấy thẻ table append vào 1 tr
    $(".tblChiTietPhieuNhap").append(trnoidung);
    loadIDLENTHE();

});

//Phương thức xử lý lấy thuộc tính attr từ thẻ tr gán xuống chỉ số phần tử các trong thuộc tính name của thẻ input
function loadIDLENTHE() {
    $(".trAppended").each(function () {
        //Lấy thuộc tính data-id của thẻ tr hiện
        var id = $(this).attr("data-id");
        var nameMaSanPham = "[" + id + "].MaSP"; //Tạo ra mã sản phẩm
        var nameSoLuongNhap = "[" + id + "].SoLuongNhap"; //Tạo ra số lượng nhập
        var nameDonGiaNhap = "[" + id + "].DonGiaNhap";   //Tạo ra đơn giá nhập
        $(this).find(".ddlSanPham").attr("name", nameMaSanPham);//Gán name cho dropdownlist
        $(this).find(".txtDonGia").attr("name", nameDonGiaNhap);//Gán name đơn giá nhập
        $(this).find(".txtSoLuong").attr("name", nameSoLuongNhap);//Gán name số lượng nhập

    })
};
//Xử lý sự kiện xóa 1 dòng từ nút delete nằm bên trong thẻ tr
//$("body").on("click", ".btnDelete", function () {
//    //Xóa phần tử cha phía ngoài
//    $(this).closest(".trAppended").remove();
//});

function CapNhapID() {   //Lấy lại tr 1
    var id_cuoi = $(".tblChiTietPhieuNhap").find(".trFirstChild").attr("data-id");
    i = parseInt(id_cuoi) + 1;
    $(".trAppended").each(function () {
        var id = i;
        $(this).attr("data-id", i);
        //Cập nhật lại id khi xóa
        var nameMaSanPham = "[" + id + "].MaSP"; //Tạo ra mã sản phẩm
        var nameSoLuongNhap = "[" + id + "].SoLuongNhap"; //Tạo ra số lượng nhập
        var nameDonGiaNhap = "[" + id + "].DonGiaNhap";   //Tạo ra đơn giá nhập
        $(this).find(".ddlSanPham").attr("name", nameMaSanPham);//Gán name cho dropdownlist
        $(this).find(".txtDonGia").attr("name", nameDonGiaNhap);//Gán name đơn giá nhập
        $(this).find(".txtSoLuong").attr("name", nameSoLuongNhap);//Gán name số lượng nhập
        i++;
    })
}

//Xử lý sự kiện xóa
$("body").delegate(".btnDelete", "click", function () {
    //Xóa phần tử cha phía ngoài
    $(this).closest("tr").remove();
    CapNhapID();
});

$("#btnNhapHang").click(function () {

    if (kiemtraDonGia() == false) {
        //Nếu tồn tại 1 giá trị bất kỳ thuộc class đơn giá không phải số, thì ngăn không cho submit về server
        return false;
    }
    if (kiemtraSoLuong() == false) {
        return false;
    }

});
//Kiểm tra đơn giá
function kiemtraDonGia() {
    var bl = true;
    //Duyệt vòng lặp each
    $(".txtDonGia").each(function () {
        var giatri = $(this).val();
        if (isNaN(giatri) == true) {
            alert("Đơn giá không hợp lệ!");
            bl = false;
            return bl;
        }
    });
    return bl;
}
function kiemtraSoLuong() {
    var bl = true;
    //Duyệt vòng lặp each
    $(".txtSoLuong").each(function () {
        var giatri = $(this).val();
        if (isNaN(giatri) == true) {
            alert("Số lượng không hợp lệ!");
            bl = false;
            return bl;
        }
    });
    return bl;
   
}
$("#btnSPHetHang").click(function () {
    $(".Sphethang").toggle(1000);
});
