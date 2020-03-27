using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SentimentRazorML.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SentimentRazor.Api
{
    [Route("api/[controller]")]
    public class AnalyzerController : ControllerBase
    {

        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public AnalyzerController(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromQuery] string text)
        {
            if (String.IsNullOrEmpty(text)) return BadRequest("Neutral");

            var input = new ModelInput { SentimentText = text };
            var prediction = _predictionEnginePool.Predict(input);
            var sentiment = Convert.ToBoolean(prediction.Prediction) ? "Toxic" : "Not Toxic";

            return Ok(sentiment);
        }

    }
}
