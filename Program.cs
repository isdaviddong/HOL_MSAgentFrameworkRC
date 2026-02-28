using Microsoft.Agents.AI;
using OpenAI;
using OpenAI.Responses;

#pragma warning disable OPENAI001 

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// 從環境變數讀取 OpenAI API 金鑰
var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
    ?? throw new InvalidOperationException("請設置 OPENAI_API_KEY 環境變數。");

// 1) 建立一個 AI Agent，指定使用的模型和角色設定
var agent = new OpenAIClient(apiKey)
    .GetResponsesClient("gpt-4.1")
    .AsAIAgent(
        name: "HelloBot",
        instructions: "你是一個友善且樂於助人的聊天機器人，叫做 HelloBot。請用中文回答問題，並且保持對話的連貫性。");

// 2) 建立一個對話 session，這樣才能保持多輪對話的上下文
var session = await agent.CreateSessionAsync();

Console.WriteLine("HelloBot：你好！輸入 /exit 離開，/reset 重新開始一段新對話。");

// 3) 進入對話迴圈
while (true)
{
    Console.Write("\n你：");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Equals("/exit", StringComparison.OrdinalIgnoreCase))
        break;

    if (input.Equals("/reset", StringComparison.OrdinalIgnoreCase))
    {
        session = await agent.CreateSessionAsync();
        Console.WriteLine("HelloBot：已重置對話，我們重新開始吧！");
        continue;
    }

    try
    {
        // 4) 每次都把同一個 session 帶入，才能多輪對談
        var reply = await agent.RunAsync(input, session: session);
        Console.WriteLine($"\nHelloBot：{reply}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[錯誤] {ex.Message}");
    }
}

Console.WriteLine("\nHelloBot：掰掰！");