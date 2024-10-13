using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.Kadry;
using Soneta.KadryPlace;
using Soneta.Types;
using Rekrutacja.Workers.Template;
using Rekrutacja.Helpers;
using Rekrutacja.Models;

[assembly: Worker(typeof(FieldTemplateWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.Template
{
    public class FieldTemplateWorker
    {
        public class FieldTemplateWorkerParametry : ContextBase
        {
            [Caption("A")]
            public string A { get; set; }
            [Caption("B")]
            public string B { get; set; }
            [Caption("Data obliczeń")]
            public Date DataObliczen { get; set; }
            [Caption("Typ figury")]
            public FigureType TypFigury { get; set; }
            public FieldTemplateWorkerParametry(Context context) : base(context)
            {
                this.DataObliczen = Date.Today;
            }
        }
        [Context]
        public Context Cx { get; set; }
        [Context]
        public FieldTemplateWorkerParametry Parametry { get; set; }
        [Action("Kalkulator pola",
           Description = "Prosty kalkulator do wyliczania pola figury",
           Priority = 10,
           Mode = ActionMode.ReadOnlySession,
           Icon = ActionIcon.Accept,
           Target = ActionTarget.ToolbarWithText)]
        public void WykonajAkcje()
        {
            DebuggerSession.MarkLineAsBreakPoint();
            Pracownik[] pracowniks = null;
            if (this.Cx.Contains(typeof(Pracownik[])))
                pracowniks = (Pracownik[])this.Cx[typeof(Pracownik[])];

            using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
            {
                using (ITransaction trans = nowaSesja.Logout(true))
                {
                    var dataObliczen = this.Parametry.DataObliczen;
                    var wynik = ActionHelper.ObliczPole(this.Parametry.A.ParseForIntValue(), this.Parametry.B.ParseForIntValue(), this.Parametry.TypFigury);
                    foreach (var p in pracowniks)
                    {
                        var pracownikZSesja = nowaSesja.Get(p);
                        pracownikZSesja.Features["Wynik"] = (double)wynik;
                        pracownikZSesja.Features["DataObliczen"] = dataObliczen;
                    }
                    trans.CommitUI();
                }
                nowaSesja.Save();
            }
        }

    }
}
