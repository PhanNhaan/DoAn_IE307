using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WatchShop.Models;

namespace WatchShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            if (NguoiDung.nguoidung.QUYEN == "ADMIN") { quyen.IsVisible = true; }
            else { quyen.IsVisible = false; }
            quyen.SelectedIndex = 0;
        }
        private void BackTapped(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private async void cmddangky_Clicked(object sender, EventArgs e)
        {

            if (txtmatkhau.Text != txtmatkhaunl.Text)
            {
                await DisplayAlert("Thông báo", "Mật khẩu nhập lại không đúng", "OK");
                return;
            }
            NguoiDung nd = new NguoiDung();

            if (NguoiDung.nguoidung.QUYEN == "ADMIN")
            {
                nd.TENDN = txttendn.Text;
                nd.PASS = txtmatkhau.Text;
                nd.EMAIL = txtEmail.Text;
                nd.SDT = txtSdt.Text;
                nd.QUYEN = quyen.SelectedItem.ToString();
            }
            else 
            {
                nd.TENDN = txttendn.Text;
                nd.PASS = txtmatkhau.Text;
                nd.EMAIL = txtEmail.Text;
                nd.SDT = txtSdt.Text;
                nd.QUYEN = "USER";
            }
            HttpClient http = new HttpClient();
            string jsonlh = JsonConvert.SerializeObject(nd);
            StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync
                (Host.url.ToString() + "api/LoginController/ThemNguoiDung", httcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            nd = JsonConvert.DeserializeObject<NguoiDung>(kqtv);
            if (nd.MAND != null || nd.MAND != "")
            {
                await DisplayAlert("Thông báo", "Thêm Người dùng thành công " + nd.TENND, "ok");
                this.Navigation.PopAsync();
            }
                
            else
                await DisplayAlert("Thông báo", "Tên Đăng Nhập Đã Có " + nd.TENDN, "ok");


        }
    }
}