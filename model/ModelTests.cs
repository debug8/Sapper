using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace model
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void MinesArowndTest() 
        {
            var model = new MineField(20,20);
            var obj = new PrivateObject(model);

            obj.SetField("_fieldWidth", 2);
            obj.SetField("_fieldHight", 2);

            obj.SetField("_field", CreateTestArray0Mine());
            var mine = obj.Invoke("MineCountAround", new object[]{0, 0});
            Assert.AreEqual(0, (int)mine);

            obj.SetField("_field", CreateTestArray3Mine());
            mine = obj.Invoke("MineCountAround", new object[] { 0, 0 });
            Assert.AreEqual(3, (int)mine);

            obj.SetField("_field", CreateTestArray3MineApper());
            mine = obj.Invoke("MineCountAround", new object[] { 1, 1 });
            Assert.AreEqual(3, (int)mine);
        }

        [TestMethod]
        public void MinesArowndBigTest() 
        {
            var model = new MineField(5,5);
            var obj = new PrivateObject(model);

            obj.SetField("_field", CreateBigTestArray());
            var mine = obj.Invoke("MineCountAround", new object[] { 2, 2 });
            Assert.AreEqual(8, (int)mine);

            mine = obj.Invoke("MineCountAround", new object[] { 0, 0 });
            Assert.AreEqual(1, (int)mine);

            mine = obj.Invoke("MineCountAround", new object[] { 0, 1 });
            Assert.AreEqual(2, (int)mine);

            mine = obj.Invoke("MineCountAround", new object[] { 0, 2 });
            Assert.AreEqual(3, (int)mine);

            mine = obj.Invoke("MineCountAround", new object[] { 1, 2 });
            Assert.AreEqual(4, (int)mine);

            mine = obj.Invoke("MineCountAround", new object[] { 4, 4 });
            Assert.AreEqual(1, (int)mine);

            mine = obj.Invoke("MineCountAround", new object[] { 3, 3 });
            Assert.AreEqual(2, (int)mine);

            mine = obj.Invoke("MineCountAround", new object[] { 3, 2 });
            Assert.AreEqual(4, (int)mine);
        }
        #region Arr1
        private FieldElement[,] CreateTestArray3Mine() 
        {
            var arr = new FieldElement[2, 2];

            arr[0, 0] = new FieldElement();
            arr[0, 1] = new FieldElement() { HasMine = true};
            arr[1, 0] = new FieldElement() { HasMine = true };
            arr[1, 1] = new FieldElement() { HasMine = true };

            return arr;
        }

        private FieldElement[,] CreateTestArray0Mine()
        {
            var arr = new FieldElement[2, 2];

            arr[0, 0] = new FieldElement();
            arr[0, 1] = new FieldElement();
            arr[1, 0] = new FieldElement();
            arr[1, 1] = new FieldElement();

            return arr;
        }

        private FieldElement[,] CreateTestArray3MineApper()
        {
            var arr = new FieldElement[2, 2];

            arr[0, 0] = new FieldElement() { HasMine = true }; 
            arr[0, 1] = new FieldElement() { HasMine = true };
            arr[1, 0] = new FieldElement() { HasMine = true };
            arr[1, 1] = new FieldElement();

            return arr;
        }
        #endregion

        #region Arr2
        private FieldElement[,] CreateBigTestArray()
        {
            var arr = new FieldElement[5, 5];

            arr[0, 0] = new FieldElement();
            arr[0, 1] = new FieldElement();
            arr[0, 2] = new FieldElement();
            arr[0, 3] = new FieldElement();
            arr[0, 4] = new FieldElement();

            arr[1, 0] = new FieldElement();
            arr[1, 1] = new FieldElement() { HasMine = true };
            arr[1, 2] = new FieldElement() { HasMine = true };
            arr[1, 3] = new FieldElement() { HasMine = true };
            arr[1, 4] = new FieldElement();

            arr[2, 0] = new FieldElement();
            arr[2, 1] = new FieldElement() { HasMine = true };
            arr[2, 2] = new FieldElement();
            arr[2, 3] = new FieldElement() { HasMine = true };
            arr[2, 4] = new FieldElement();

            arr[3, 0] = new FieldElement();
            arr[3, 1] = new FieldElement() { HasMine = true };
            arr[3, 2] = new FieldElement() { HasMine = true };
            arr[3, 3] = new FieldElement() { HasMine = true };
            arr[3, 4] = new FieldElement();

            arr[4, 0] = new FieldElement();
            arr[4, 1] = new FieldElement();
            arr[4, 2] = new FieldElement();
            arr[4, 3] = new FieldElement();
            arr[4, 4] = new FieldElement();

            return arr;
        }



        #endregion
    }
}
