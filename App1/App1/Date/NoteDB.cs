using System.Collections.Generic;

using SQLite;
using Notes.Models;
using System.Threading.Tasks;

namespace Notes.Date
{
    public class NotesDB
    {
        readonly SQLiteAsyncConnection db;

        public NotesDB(string ConnectionString)
        {
            db = new SQLiteAsyncConnection(ConnectionString);

            db.CreateTableAsync<Note>().Wait();
        }


        // возвращает все списки
        public Task<List<Note>> GetNotesAsync()
        {
            return db.Table<Note>().ToListAsync();
        }

        // возвращает 1 список
        public Task<Note> GetNoteAsync(int id)
        {
            return db.Table<Note>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }


        // добавление/изменение уже сухествующего списка
        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID != 0) // есть ли список
            { // есть
                return db.UpdateAsync(note); // обновление списка
            }
            else
            { // нет
                return db.InsertAsync(note); // добавление списка
            }
        }

        // удаление списка
        public Task<int> DeleteNoteAsync(Note note)
        {
            return db.DeleteAsync(note);
        }
    }
}
