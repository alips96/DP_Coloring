using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Text;

namespace Dp_Coloring
{
    public class Test_MainCalculation
    {
        Main_Calculation mainCalculationScript;

        [UnityTest]
        public IEnumerator Test_MainCalculationWithEnumeratorPasses()
        {
            SetInitialRefrences();
            yield return null;

            string actual;

            //input = {5,6} , k = 0
            StringBuilder input = new StringBuilder("56");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("616", actual);

            //input = {6,6} , k = 0
            input = new StringBuilder("66");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("606", actual);

            //input = {4,5,6} , k = 0
            input = new StringBuilder("456");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("56256", actual);

            //input = {4,5,6} , k = 1
            input = new StringBuilder("456");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("626", actual);

            //input = {6,6,6} , k = 1
            input = new StringBuilder("666");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("616", actual);

            //input = {6,6,6} , k = 0
            input = new StringBuilder("666");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("66166", actual);

            //input = {6,5,6} , k = 0
            input = new StringBuilder("656");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("56156", actual);

            //input = {6,5,4,3} , k = 0
            input = new StringBuilder("6543");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("5433543", actual);

            //input = {6,5,4,6} , k = 0
            input = new StringBuilder("6546");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("5462546", actual);

            //input = {6,5,4,6} , k = 1
            input = new StringBuilder("6546");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("46246", actual);

            //input = {6,5,4,6} , k = 2
            input = new StringBuilder("6546");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("626", actual);

            //input = {6,6,6,6} , k = 0
            input = new StringBuilder("6666");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("6662666", actual);

            //input = {6,6,6,6} , k = 1
            input = new StringBuilder("6666");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("66066", actual);

            //input = {6,6,6,7} , k = 2
            input = new StringBuilder("6667");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("737", actual);

            //input = {4,3,8,4,3} , k = 2
            input = new StringBuilder("43843");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("43143", actual);

            //input = {1,2,2,1,3} , k = 1
            input = new StringBuilder("12213");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("2133213", actual);

            //input = {2,2,2,1,3} , k = 1
            input = new StringBuilder("22213");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("2132213", actual);

            //input = {2,2,2,2,3} , k = 1
            input = new StringBuilder("22223");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("2231223", actual);

            //input = {2,2,2,2,2} , k = 1
            input = new StringBuilder("22222");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("2221222", actual);

            //input = {2,2,2,2,2} , k = 2
            input = new StringBuilder("22222");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("22122", actual);

            //input = {2,2,2,2,2} , k = 3
            input = new StringBuilder("22222");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 3);
            Assert.AreEqual("232", actual);

            //input = {1,2,3,2,2} , k = 1
            input = new StringBuilder("12322");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("3222322", actual);

            //input = {4,3,8,4,3,8} , k = 2
            input = new StringBuilder("438438");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("4380438", actual);

            //input = {4,3,8,4,3,8} , k = 0
            input = new StringBuilder("438438");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 0);
            Assert.AreEqual("38438438438", actual);

            //input = {4,3,3,8,4,3} , k = 1
            input = new StringBuilder("433843");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 1);
            Assert.AreEqual("384323843", actual);

            //input = {4,4,4,4,4,4} , k = 2
            input = new StringBuilder("444444");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("4440444", actual);

            //input = {4,3,8,5,2,5,2,4,3,8} , k = 4
            input = new StringBuilder("4385252438");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 4);
            Assert.AreEqual("52438452438", actual);

            //input = {1203438438412} , k = 6
            input = new StringBuilder("1203438438412");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 6);
            Assert.AreEqual("4384127438412", actual);

            //input = {438254380256} , k = 4
            input = new StringBuilder("438254380256");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 4);
            Assert.AreEqual("438025624380256", actual);

            //input = {395438438539} , k = 5
            input = new StringBuilder("395438438539");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 5);
            Assert.AreEqual("4385396438539", actual);

            //input = {1203438438412} , k = 6
            input = new StringBuilder("1203438438412");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 6);
            Assert.AreEqual("4384127438412", actual);

            //input = {22439857851432} , k = 6
            input = new StringBuilder("22439857851432");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 6);
            Assert.AreEqual("785143297851432", actual);

            //input = {49532783} , k = 4
            input = new StringBuilder("49532783");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 4);
            Assert.AreEqual("7834783", actual);

            //input = {24372} , k = 2
            input = new StringBuilder("24372");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("72372", actual);

            //input = {8438217358438217351} , k = 8
            input = new StringBuilder("8438217358438217351");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 8);
            Assert.AreEqual("843821735118438217351", actual);

            //input = {2555355545} , k = 4
            input = new StringBuilder("2555355545");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 4);
            Assert.AreEqual("55545355545", actual);

            //input = {223222} , k = 2
            input = new StringBuilder("223222");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("2221222", actual);

            //input = {457648} , k = 2
            input = new StringBuilder("457648");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 2);
            Assert.AreEqual("6483648", actual);

            //input = {7777777} , k = 3
            input = new StringBuilder("7777777");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 3);
            Assert.AreEqual("7771777", actual);

            //input = {7777777} , k = 3
            input = new StringBuilder("7777777");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 3);
            Assert.AreEqual("7771777", actual);

            //input = {9243724579} , k = 4
            input = new StringBuilder("9243724579");
            actual = mainCalculationScript.CalculateAndReturnSymmetricalList(input, 4);
            Assert.AreEqual("24579324579", actual);
        }

        private void SetInitialRefrences()
        {
            mainCalculationScript = Object.Instantiate(Resources.Load<GameObject>("GameManager")).GetComponent<Main_Calculation>();

            if (mainCalculationScript == null)
            {
                Debug.LogWarning("mainCalculationScript is null");
            }
        }
    }
}

