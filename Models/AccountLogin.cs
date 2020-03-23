﻿using System.ComponentModel.DataAnnotations;

namespace ImportShopCore.Models.Account {
  public class AccountLogin {
    [Required]
    [StringLength(46, ErrorMessage = "Длина Telegram токена составляет 46 символов")]
    public string TelegramToken { get; set; }
  }
}