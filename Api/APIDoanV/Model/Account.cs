﻿using System;
using System.Collections.Generic;

namespace APIDoanV.Model;

public partial class Account
{
    public int MaTaiKhoan { get; set; }

    public int Idkh { get; set; }

    public string? Anh { get; set; }

    public string? TaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public bool? TrangThai { get; set; }

    public string? LoaiQuyen { get; set; }

    public virtual KhachHang IdkhNavigation { get; set; } = null!;
}
