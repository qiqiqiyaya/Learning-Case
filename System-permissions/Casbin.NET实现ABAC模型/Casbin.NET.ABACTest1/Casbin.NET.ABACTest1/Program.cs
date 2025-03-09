namespace Casbin.NET.ABACTest1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 初始化 Casbin Enforcer
            var enforcer = new Enforcer("model.conf", "policy.csv");

            // 定义测试案例
            var testCases = new[]
            {
                // 案例 1：部门匹配、安全等级足够、工作时间（应允许读取）
                new
                {
                    User = new User { Department = "Finance", SecurityLevel = 4 },
                    Doc = new Document { Department = "Finance", ClassificationLevel = 3 },
                    Action = "read",
                    EnvTime = new DateTime(2023, 10, 1, 10, 0, 0), // 10:00 AM
                    Expected = true
                },
                // 案例 2：部门不匹配（应拒绝读取）
                new
                {
                    User = new User { Department = "HR", SecurityLevel = 4 },
                    Doc = new Document { Department = "Finance", ClassificationLevel = 3 },
                    Action = "read",
                    EnvTime = new DateTime(2023, 10, 1, 10, 0, 0),
                    Expected = false
                },
                // 案例 3：安全等级不足（应拒绝读取）
                new
                {
                    User = new User { Department = "Finance", SecurityLevel = 2 },
                    Doc = new Document { Department = "Finance", ClassificationLevel = 3 },
                    Action = "read",
                    EnvTime = new DateTime(2023, 10, 1, 10, 0, 0),
                    Expected = false
                },
                // 案例 4：非工作时间（应拒绝读取）
                new
                {
                    User = new User { Department = "Finance", SecurityLevel = 4 },
                    Doc = new Document { Department = "Finance", ClassificationLevel = 3 },
                    Action = "read",
                    EnvTime = new DateTime(2023, 10, 1, 20, 0, 0), // 8:00 PM
                    Expected = false
                },
                // 案例 5：允许写入（部门匹配且安全等级≥3）
                new
                {
                    User = new User { Department = "Finance", SecurityLevel = 3 },
                    Doc = new Document { Department = "Finance", ClassificationLevel = 1 },
                    Action = "write",
                    EnvTime = new DateTime(2023, 10, 1, 10, 0, 0),
                    Expected = true
                }
            };

            // 执行测试
            foreach (var testCase in testCases)
            {
                var env = new Dictionary<string, object> { { "Time", testCase.EnvTime } };
                bool result = enforcer.Enforce(
                    testCase.User,
                    testCase.Doc,
                    testCase.Action,
                    env
                );

                Console.WriteLine($"测试结果: {result == testCase.Expected} | " +
                                  $"预期: {testCase.Expected}, 实际: {result} | " +
                                  $"场景: 用户部门={testCase.User.Department}, " +
                                  $"文档部门={testCase.Doc.Department}, " +
                                  $"操作={testCase.Action}, " +
                                  $"时间={testCase.EnvTime:HH:mm}");
            }

            Console.ReadLine();
        }
    }
}
