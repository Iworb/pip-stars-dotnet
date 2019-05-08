using PipServices3.Commons.Commands;
using PipServices3.Commons.Convert;
using PipServices3.Commons.Data;
using PipServices3.Commons.Validate;
using Stars.Data.Version1;

namespace Stars.Logic
{
    public class StarsCommandSet : CommandSet
    {
        private IStarsController _controller;

        public StarsCommandSet(IStarsController controller)
        {
            _controller = controller;

            AddCommand(MakeGetStarsCommand());
            AddCommand(MakeGetStarByIdStarsCommand());
            AddCommand(MakeCreateStarCommand());
            AddCommand(MakeUpdateStarCommand());
            AddCommand(MakeDeleteStarByIdCommand());
        }

        private ICommand MakeGetStarsCommand()
        {
            return new Command(
                "get_stars",
                new ObjectSchema()
                    .WithOptionalProperty("filter", new FilterParamsSchema())
                    .WithOptionalProperty("paging", new PagingParamsSchema()),
                async (correlationId, parameters) =>
                {
                    var filter = FilterParams.FromValue(parameters.Get("filter"));
                    var paging = PagingParams.FromValue(parameters.Get("paging"));
                    return await _controller.GetStarsAsync(correlationId, filter, paging);
                });
        }

        private ICommand MakeGetStarByIdStarsCommand()
        {
            return new Command(
                "get_star_by_id",
                new ObjectSchema()
                    .WithRequiredProperty("star_id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var id = parameters.GetAsString("star_id");
                    return await _controller.GetStarByIdAsync(correlationId, id);
                });
        }

        private ICommand MakeCreateStarCommand()
        {
            return new Command(
                "create_star",
                new ObjectSchema()
                    .WithRequiredProperty("star", new StarV1Schema()),
                async (correlationId, parameters) =>
                {
                    var star = ConvertToStar(parameters.GetAsObject("star"));
                    return await _controller.CreateStarAsync(correlationId, star);
                });
        }

        private ICommand MakeUpdateStarCommand()
        {
            return new Command(
               "update_star",
               new ObjectSchema()
                    .WithRequiredProperty("star", new StarV1Schema()),
               async (correlationId, parameters) =>
               {
                   var star = ConvertToStar(parameters.GetAsObject("star"));
                   return await _controller.UpdateStarAsync(correlationId, star);
               });
        }

        private ICommand MakeDeleteStarByIdCommand()
        {
            return new Command(
               "delete_star_by_id",
               new ObjectSchema()
                   .WithRequiredProperty("star_id", TypeCode.String),
               async (correlationId, parameters) =>
               {
                   var id = parameters.GetAsString("star_id");
                   return await _controller.DeleteStarByIdAsync(correlationId, id);
               });
        }

        private StarV1 ConvertToStar(object value)
        {
            return JsonConverter.FromJson<StarV1>(JsonConverter.ToJson(value));
        }
    }
}