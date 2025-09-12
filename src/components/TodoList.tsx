import { useEffect, useState } from "react";
import api from "../lib/api";

interface Todo {
  id: number;
  title: string;
  isDone: boolean;
}

export default function TodoList() {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [newTodo, setNewTodo] = useState("");

  // Hämta todos vid start
  useEffect(() => {
    loadTodos();
  }, []);

  const loadTodos = async () => {
    try {
      const res = await api.get<Todo[]>("/api/todos");
      setTodos(res.data);
    } catch (err) {
      console.error("Kunde inte hämta todos", err);
    }
  };

  // Lägg till todo
  const addTodo = async () => {
    if (!newTodo.trim()) return;

    try {
      const res = await api.post<number>("/api/todos", {
        title: newTodo,
      });
      // Lägg in direkt i listan
      setTodos([...todos, { id: res.data, title: newTodo, isDone: false }]);
      setNewTodo("");
    } catch (err) {
      console.error("Kunde inte lägga till todo", err);
    }
  };

  // Uppdatera (toggle IsDone)
  const toggleTodo = async (todo: Todo) => {
    try {
      await api.put(`/api/todos/${todo.id}`, {
        id: todo.id,
        title: todo.title,
        isDone: !todo.isDone,
      });
      setTodos(
        todos.map((t) =>
          t.id === todo.id ? { ...t, isDone: !t.isDone } : t
        )
      );
    } catch (err) {
      console.error("Kunde inte uppdatera todo", err);
    }
  };

  // Ta bort todo
  const deleteTodo = async (id: number) => {
    try {
      await api.delete(`/api/todos/${id}`);
      setTodos(todos.filter((t) => t.id !== id));
    } catch (err) {
      console.error("Kunde inte ta bort todo", err);
    }
  };

  return (
    <div>
      <h2>📋 Mina Todos</h2>

      {/* Input för ny todo */}
      <div style={{ marginBottom: "1rem" }}>
        <input
          value={newTodo}
          onChange={(e) => setNewTodo(e.target.value)}
          placeholder="Ny todo..."
        />
        <button onClick={addTodo}>Lägg till</button>
      </div>

      {/* Lista todos */}
      <ul>
        {todos.map((todo) => (
          <li
            key={todo.id}
            style={{
              textDecoration: todo.isDone ? "line-through" : "none",
              marginBottom: "0.5rem",
            }}
          >
            <span onClick={() => toggleTodo(todo)} style={{ cursor: "pointer" }}>
              {todo.title}
            </span>
            <button
              onClick={() => deleteTodo(todo.id)}
              style={{ marginLeft: "1rem" }}
            >
              ❌ Ta bort
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
