namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IListItemService: IGetAllListsService, ICreateListService, IUpdateListService, IDeleteListService, IGetListService, IGetAllListsByType
    {
    }
}
