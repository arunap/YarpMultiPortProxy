# YARP Multi-Port Proxy

A .NET 8 reverse proxy using YARP that listens on different ports and forwards to different internal middle-tier services.

## Ports & Behavior

- `http://<server-ip>:27000` → Proxies to `middle1.internal` **using an upstream HTTP proxy**
- `http://<server-ip>:27001` → Proxies to `middle2.internal` **directly without proxy**

## Configuration

- Update `appsettings.json` to reflect actual internal addresses.
- Set `http://your-proxy-host:your-proxy-port` in `Program.cs`.

## Run

```bash
dotnet restore
dotnet run
```

## Notes

- Replace internal URLs and proxy address as per your environment.
