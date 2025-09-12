import { useEffect, useState } from "react";
import api from "../lib/api";

export default function Ping() {
  const [msg, setMsg] = useState<string>("(loading...)");
  const [err, setErr] = useState<string | null>(null);

  useEffect(() => {
    api.get("/api/ping")
      .then(r => setMsg(String(r.data)))
      .catch(e => setErr(e?.message ?? "error"));
  }, []);

  if (err) return <div style={{ color: "crimson" }}>Ping failed: {err}</div>;
  return <div>Ping: {msg}</div>;
}
