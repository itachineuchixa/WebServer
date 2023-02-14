import httpx

req = httpx.get("https://dexscreener.com/ethereum/0xeedff72a683058f8ff531e8c98575f920430fdc5")
print(req.text)
