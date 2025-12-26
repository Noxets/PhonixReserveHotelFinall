using System.Net;
using Hamkare.Common.Entities.Generics;
using Hamkare.Common.Interface.Entities;

namespace Hamkare.Common.Entities.General;

public class Redirect : RootEntity, IRootEntity
{
    public string Source { get; set; }
    
    public HttpStatusCode Code { get; set; }
    
    public string Destination { get; set; }
    
    public override bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        return errors.Any();
    }
}