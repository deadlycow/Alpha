//using Business.Factories;
//using Business.Models;
//using Data.Interfaces;

//namespace Business.Services;
//public class TitleService(ITitleRepsitory titleRepsitory)
//{
//  private readonly ITitleRepsitory _titleRepsitory = titleRepsitory;

//  public async Task<IEnumerable<Title>> GeAllAsync()
//  {
//    var entities = await _titleRepsitory.GetAllAsync();
//    if (entities == null)
//      return null!;
//    var titles = TitleFactory.CreateList(entities);
//    return titles;
//  }
//}
