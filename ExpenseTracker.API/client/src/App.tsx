import { useEffect, useState } from "react";

interface Expense {
    id: string;
    title: string;
    amount: number;
    date: string;
    categoryName: string;
}

interface Category {
    id: string;
    name: string;
}

function App() {
    const [expenses, setExpenses] = useState<Expense[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);

    const [title, setTitle] = useState("");
    const [amount, setAmount] = useState(0);
    const [date, setDate] = useState("");
    const [categoryId, setCategoryId] = useState("");

    // Загрузка категорий при старте
    useEffect(() => {
        fetch("/api/Categories")
            .then((res) => res.json())
            .then((data) => setCategories(data));
    }, []);

    const loadAllExpenses = async () => {
        const res = await fetch("/api/Expenses/all");
        const data = await res.json();
        setExpenses(data);
    };

    const submit = async () => {
        await fetch("/api/Expenses", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ title, amount, date, categoryId }),
        });

        await loadAllExpenses(); // обновим список
        setTitle("");
        setAmount(0);
        setDate("");
        setCategoryId("");
    };

    return (
        <div className="p-4">
            <h1 className="text-xl font-bold mb-4">Добавить расход</h1>
            <div className="flex gap-2 mb-4">
                <input
                    placeholder="Заголовок"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    className="border p-2"
                />
                <input
                    placeholder="Сумма"
                    type="number"
                    value={amount}
                    onChange={(e) => setAmount(+e.target.value)}
                    className="border p-2"
                />
                <input
                    placeholder="Дата"
                    type="date"
                    value={date}
                    onChange={(e) => setDate(e.target.value)}
                    className="border p-2"
                />
                <select
                    value={categoryId}
                    onChange={(e) => setCategoryId(e.target.value)}
                    className="border p-2"
                >
                    <option value="">Категория</option>
                    {categories.map((c) => (
                        <option key={c.id} value={c.id}>
                            {c.name}
                        </option>
                    ))}
                </select>
                <button onClick={submit} className="bg-blue-500 text-white px-4 py-2 rounded">
                    Добавить
                </button>
            </div>

            <button
                onClick={loadAllExpenses}
                className="bg-gray-600 text-white px-4 py-2 mb-4 rounded"
            >
                Показать все расходы
            </button>

            <ul className="space-y-2">
                {expenses.map((e) => (
                    <li key={e.id} className="border p-2">
                        <div>{e.title}</div>
                        <div>
                            {e.amount} — {new Date(e.date).toLocaleDateString()} — {e.categoryName}
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
