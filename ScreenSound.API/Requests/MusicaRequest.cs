using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ScreenSound.API.Requests;

public record MusicaRequest([Required] string nome, [Required] int ArtistaId, int anoLancamento);

