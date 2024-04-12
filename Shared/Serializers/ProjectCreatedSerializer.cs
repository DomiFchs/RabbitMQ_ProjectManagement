using System.Text.Json;
using System.Text.Json.Serialization;
using Shared.Entities.Projects.AProjects;
using Shared.Entities.Projects.Managements;
using Shared.Entities.Projects.RequestProjects;

namespace Shared.Serializers;

public class ProjectCreatedSerializer : JsonConverter<AProjectCreated> {
    public override AProjectCreated? Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var isManagementProject = TryGetPropertyIgnoreCase(root, "LawType", out _);
        
        var serializerCfg = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        
        if (isManagementProject) {
            return JsonSerializer.Deserialize<ManagementProjectCreated>(root.GetRawText(), serializerCfg);
        }

        var isRequestFundingProject = TryGetPropertyIgnoreCase(root, "IsFfgFunded", out _) &&
                                      TryGetPropertyIgnoreCase(root, "IsFwfFunded", out _);

        if(!isRequestFundingProject) throw new JsonException("Unknown project type.");
        
        return JsonSerializer.Deserialize<RequestFundingProjectCreated>(root.GetRawText(), serializerCfg);
    }

    public override void Write(Utf8JsonWriter writer, AProjectCreated value, JsonSerializerOptions options) {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
    
    private static bool TryGetPropertyIgnoreCase(JsonElement element, string propertyName, out JsonElement value)
    {
        foreach (var prop in element.EnumerateObject().Where(prop => string.Equals(prop.Name, propertyName, StringComparison.OrdinalIgnoreCase))) {
            value = prop.Value;
            return true;
        }
        value = default;
        return false;
    }
}