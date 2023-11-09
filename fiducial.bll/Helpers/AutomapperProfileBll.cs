using AutoMapper;
using fiducial.bll.Models;
using fiducial.dal.Models;

namespace fiducial.bll.Helpers;

public class AutomapperProfileBll: Profile
{
    public AutomapperProfileBll()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookAddDto, Book>();
        CreateMap<BookUpdateDto, Book>();
    }
}