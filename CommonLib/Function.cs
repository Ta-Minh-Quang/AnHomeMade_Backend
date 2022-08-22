using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Function
    {
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static int GetFromToPaging(int currentPage, int itemPerPage, out int endItem)
        {
            endItem = -1;
            try
            {
                int _StartItem = itemPerPage * (currentPage - 1) + 1;
                endItem = itemPerPage * currentPage ;
                return _StartItem;
            }
            catch (Exception ex)
            {
                endItem = -1;
                return -1;
            }
        }

        public static string PagingData(int currentPage, int itemPerPage, int totalItem, string functionJavascript = "changePage", bool hasExportBtn = false)
        {
            try
            {
                int _end = -1;
                int _start = GetFromToPaging(currentPage, itemPerPage, out _end);
                string pStrPaging = "";
                double _dobTotalItem = Convert.ToDouble(totalItem);
                int _TotalPage = (int)Math.Ceiling((double)totalItem / itemPerPage);
                pStrPaging = WritePagingV2(_start, _end, _TotalPage, currentPage, functionJavascript, totalItem, itemPerPage, "bản ghi", hasExportBtn);
                return pStrPaging;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string WritePagingV2(int start, int end, int pageCount, int currentPage, string functionJavascript, int totalItem, int pageSize, string loaiBanGhi, bool hasExportBtn = false)
        {
            try
            {
                string strPage = "";

                strPage += "<div class='box-paging'>";
                strPage += "<div class='chose-record-perpage'>";
                strPage += "<span>Số bản ghi</span>";
                strPage += "<input type = 'text' class='txtRecordPerpage' id='txtItemPerpage' value='" + pageSize.ToString()+"'/></div>";
                strPage += "<div class=\"d-flex\" id='page'>";
                strPage += "<div class=\"number-record\" id='d_total_rec'>Hiển thị:  " + start.ToString() + " - " + end.ToString() + " / " + totalItem + " bản ghi </div>";
                strPage += "<div class=\"div-btn-paging\" id='d_number_of_page'>";
                //có nhỏ hơn hoặc bằng 1 trang
                if (pageCount <= 1)
                {
                    strPage += "</div>";
                    strPage += "</div>";
                    return strPage;
                }

                
                // Trang hiện tại lớn hơn 1
                if (currentPage > 1)
                {
                    strPage += "<button onclick=\"" + functionJavascript + "(1)\"><span id=\"back\"> << </span></button>";
                    strPage += "<button onclick=\"" + functionJavascript + "(" + (currentPage - 1) + ")\"><span> < </span></button>";
                }

                //có nhỏ hơn hoặc bằng 5 trang
                if (pageCount <= 5)
                {
                    for (int j = 0; j < pageCount; j++)
                    {
                        if (currentPage == (j + 1))
                        {
                            strPage += "<button onclick=\"" + functionJavascript + "(" + (j + 1) + ")\"><span class='a-active' id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></button>";
                        }
                        else
                        {
                            strPage += "<button onclick=\"" + functionJavascript + "(" + (j + 1) + ")\"><span id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></button>";
                        }
                    }
                }
                else // có nhiều hơn 5 trang, chỉ hiển thị 5 btn một
                {
                    string cl = "";
                    int t;
                    int pagePreview = 0;
                    //nếu đang ở trang 2 thì vẽ đc có 1 trang trước nó nên sẽ vẽ thêm 3 trang phía sau
                    //default là vẽ 2 trang trc 2 trang sau
                    int soTrangVeLui = 2;
                    if ((pageCount - currentPage) == 1)
                    {
                        soTrangVeLui = soTrangVeLui + 1;
                    }
                    else if ((pageCount - currentPage) == 0)
                    {
                        soTrangVeLui = soTrangVeLui + 2;
                    }

                    for (t = currentPage - soTrangVeLui; t <= currentPage; t++) //ve truoc 2 trang
                    {
                        if (t < 1) continue;
                        cl = t == currentPage ? "a-active" : "";
                        strPage += t == currentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                    : "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        pagePreview++;
                    }
                    t = 0;
                    cl = "";

                    if (currentPage == 1) //truong hop trang dau tien thi ve du 5 trang
                    {
                        for (t = currentPage + 1; t < currentPage + 5; t++)
                        {
                            if (t >= t + 5 || t > pageCount) continue;
                            cl = t == currentPage ? "a-active" : "";
                            strPage += t == currentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                     : "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        }
                    }
                    else if (currentPage > 1)  //truogn hop ma la trang lon hon 1 thi se ve 4 trang ke tiep + 1 trang truoc
                    {
                        int incr = 5 - (pagePreview - 1);
                        for (t = currentPage + 1; t < currentPage + incr; t++)
                        {
                            if (t >= t + incr || t > pageCount) continue;
                            cl = t == currentPage ? "a-active" : "";
                            strPage += t == currentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                     : "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        }
                    }
                }

                if (currentPage < pageCount)
                {
                    strPage += "<button onclick=\"" + functionJavascript + "(" + (currentPage + 1) + ")\"><span id=\"next\"  href=\"\">></span></button>";
                    strPage += "<button onclick=\"" + functionJavascript + "(" + (pageCount.ToString()) + ")\"><span> >> </span></button>";
                }

                strPage += "</div>";
                strPage += "</div>";
                strPage += "</div>";
                return strPage;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
