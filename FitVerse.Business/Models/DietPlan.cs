using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class DietPlan
    {
        public int Id { get; set; }
        public double TotalCal { get; set; }
        public double ProteinInGrams { get; set; }
        public double CarbInGrams { get; set; }
        public double FatsInGrams { get; set; }

        public string ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public string CoachId { get; set; }
        public virtual Coach? Coach { get; set; }

        // نشاط العميل: يمكن قيم مثل 1.2, 1.375, 1.55, 1.725, 1.9
        public double ActivityMultiplier { get; set; } = 1.2;
        public string Goal { get; set; } = "Maintain";

        /*النسخة المختصرة:
        أفضل حل: دمج الطريقتين: التطبيق يقترح القيم تلقائيًا، والمدرب يستطيع تعديلها عند الحاجة.
التطبيق يحسب السعرات والنسب تلقائيًا حسب بيانات العميل (الوزن، الطول، العمر، الجنس) ومستوى النشاط والهدف (خسارة وزن/زيادة عضلات) باستخدام معادلة TDEE:

BMR للرجال: 10*Weight + 6.25*Height - 5*Age + 5

BMR للنساء: 10*Weight + 6.25*Height - 5*Age - 161

TDEE: BMR * ActivityMultiplier

تقسيم السعرات: بروتين 30%، كربوهيدرات 50%، دهون 20%.

مميزاته: سريع وموحد ويقلل الخطأ البشري.
عيوبه: لا يأخذ تفضيلات العميل أو المدرب إلا إذا سمحت بالتعديل.

*/
    }


}
