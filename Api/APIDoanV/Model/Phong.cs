﻿using System;
using System.Collections.Generic;

namespace APIDoanV.Model;

public partial class Phong
{
    public int Id { get; set; }

    public string TenPhong { get; set; } = null!;

    public int? IdloaiPhong { get; set; }

    public int? Dongia { get; set; }

    public string? Anh { get; set; }

    public bool? Trangthai { get; set; }

    public DateTime? NgayThem { get; set; }

    public string? NguoiTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? NguoiCapNhat { get; set; }

    public string? Mota { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<ChitietDatPhong> ChitietDatPhongs { get; } = new List<ChitietDatPhong>();

    public virtual ICollection<HoaDonBanHang> HoaDonBanHangs { get; } = new List<HoaDonBanHang>();

    public virtual LoaiPhong? IdloaiPhongNavigation { get; set; }

    public virtual ICollection<Lapdat> Lapdats { get; } = new List<Lapdat>();
}
