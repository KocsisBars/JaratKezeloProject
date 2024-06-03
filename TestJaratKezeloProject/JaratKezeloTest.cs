using JaratKezeloProject;

namespace TestJaratKezeloProject
{
    [TestFixture]
    public class JaratKezeloTests
    {
        [Test]
        public void UjJarat_UjJaratHozzaad()
        {
            var jaratKezelo = new JaratKezelo();
            jaratKezelo.UjJarat("J123", "BUD", "NYC", DateTime.Now);

            var result = jaratKezelo.MikorIndul("J123");
            Assert.NotNull(result);
        }

        [Test]
        public void UjJarat_LetezoJarat()
        {
            var jaratKezelo = new JaratKezelo();
            jaratKezelo.UjJarat("J123", "BUD", "NYC", DateTime.Now);

            Assert.Throws<ArgumentException>(() => jaratKezelo.UjJarat("J123", "BUD", "NYC", DateTime.Now));
        }

        [Test]
        public void Keses_KesesHozzaad()
        {
            var jaratKezelo = new JaratKezelo();
            jaratKezelo.UjJarat("J123", "BUD", "NYC", DateTime.Now);

            jaratKezelo.Keses("J123", 30);
            var result = jaratKezelo.MikorIndul("J123");

            Assert.AreEqual(30, (result - DateTime.Now).TotalMinutes, 1);
        }

        [Test]
        public void Keses_NegativOsszKeses()
        {
            var jaratKezelo = new JaratKezelo();
            var indulas = DateTime.Now;
            jaratKezelo.UjJarat("J123", "BUD", "NYC", indulas);

            var ex = Assert.Throws<NegativKesesException>(() => jaratKezelo.Keses("J123", -10));
            Assert.That(ex.Message, Is.EqualTo("A késés nem lehet negatív!"));
        }
        [Test]
        public void Keses_EngedNegativKesesHozzaad()
        {
            var jaratKezelo = new JaratKezelo();
            var indulas = DateTime.Now;
            jaratKezelo.UjJarat("J123", "BUD", "NYC", indulas);
            jaratKezelo.Keses("J123", 20);
            jaratKezelo.Keses("J123", -10);

            var result = jaratKezelo.MikorIndul("J123");
            Assert.AreEqual(indulas.AddMinutes(10), result);
        }

        [Test]
        public void MikorIndul_PontosIdo()
        {
            var jaratKezelo = new JaratKezelo();
            var indulas = DateTime.Now;
            jaratKezelo.UjJarat("J123", "BUD", "NYC", indulas);

            jaratKezelo.Keses("J123", 30);
            var result = jaratKezelo.MikorIndul("J123");

            Assert.AreEqual(indulas.AddMinutes(30), result);
        }

        [Test]
        public void JaratokRepuloterrol_ReturnJoJarat()
        {
            var jaratKezelo = new JaratKezelo();
            jaratKezelo.UjJarat("J123", "BUD", "NYC", DateTime.Now);
            jaratKezelo.UjJarat("J124", "BUD", "LON", DateTime.Now);
            jaratKezelo.UjJarat("J125", "NYC", "BUD", DateTime.Now);

            var result = jaratKezelo.JaratokRepuloterrol("BUD");

            Assert.Contains("J123", result);
            Assert.Contains("J124", result);
            Assert.That(result, Does.Not.Contain("J125"));
        }
    }
}