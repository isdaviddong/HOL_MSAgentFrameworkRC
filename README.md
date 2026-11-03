# msagent

一個使用 [Microsoft Agent Framework](https://github.com/microsoft/agent-framework) 與 [OpenAI API](https://platform.openai.com/) 建立的多輪對話命令列聊天機器人。

## 功能

- 透過 OpenAI `gpt-4.1` 模型進行對話
- 支援多輪對話（保持上下文連貫性）
- 輸入 `/reset` 可重置對話，開始新的一輪
- 輸入 `/exit` 可離開程式

## 環境需求

- [.NET 10.0](https://dotnet.microsoft.com/download) 或更新版本
- 有效的 [OpenAI API 金鑰](https://platform.openai.com/api-keys)

## 快速開始

### 1. 設定 API 金鑰

**僅限目前工作階段：**

```powershell
$env:OPENAI_API_KEY = "sk-..."
```
or
```bash
export OPENAI_API_KEY="sk-..."
```

**永久設定（寫入使用者環境變數）：**

```powershell
[System.Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-...", "User")
```

> 永久設定後需重新開啟終端機才會生效。

### 2. 建置並執行

```powershell
dotnet run
```

## 使用方式

程式啟動後即可直接輸入文字與機器人對話：

```
HelloBot：你好！輸入 /exit 離開，/reset 重新開始一段新對話。

你：你好，請介紹一下你自己
HelloBot：你好！我是 HelloBot，一個友善且樂於助人的聊天機器人...

你：/reset
HelloBot：已重置對話，我們重新開始吧！

你：/exit
HelloBot：掰掰！
```

| 指令 | 說明 |
|------|------|
| `/reset` | 重置對話，清除所有上下文 |
| `/exit` | 離開程式 |

## 套件相依

| 套件 | 版本 |
|------|------|
| `Microsoft.Agents.AI.OpenAI` | 1.27.0 |

此版本使用 `GetResponsesClient()` 建立 Responses client，並在 `AsAIAgent(model: "gpt-4.1", ...)` 指定模型。
