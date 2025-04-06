using System.Collections.Concurrent;
using Hl7.Fhir.Model;

namespace 
{
    public class FhirInMemoryStore
    {
        private ConcurrentDictionary<string, Communication> _communications 
            = new ConcurrentDictionary<string, Communication>();

        public Communication? GetCommunication(string id)
        {
            _communications.TryGetValue(id, out var comm);
            return comm;
        }

        public IEnumerable<Communication> GetAllCommunications() => _communications.Values;

        public Communication CreateCommunication(Communication comm)
        {
            if (string.IsNullOrEmpty(comm.Id))
            {
                comm.Id = Guid.NewGuid().ToString("N");
            }
            _communications[comm.Id] = comm;
            return comm;
        }
    }
}
