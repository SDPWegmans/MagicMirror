using MagicMirror.DTO.Note;
using MagicMirror.Web;
using System;
using System.Threading.Tasks;

namespace MirrorWebService.Managers
{
    public class NoteManager
    {
        public async Task<Note[]> GetData()
        {
            ServiceManager noteServiceManager =
                new ServiceManager(new Uri("http://mirrorservice.azurewebsites.net/api/notes"));
            var response =  await noteServiceManager.CallService<Note[]>();
            return response;
        }

        
    }
}
