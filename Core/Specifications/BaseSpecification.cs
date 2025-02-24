using System.Linq.Expressions;

namespace Core.Interfaces;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification():this(criteria: null)
    {
    }
    public Expression<Func<T, bool>>? Criteria =>  criteria;
    public Expression<Func<T, object>>? OrderByAsc { get; private set; }
    public Expression<Func<T, object>>? OrderByDesc { get; private set;}
    public bool IsDistinct { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    protected void AddOrderByAsc(Expression<Func<T, object>> orderByAscExpression)
    {
        OrderByAsc = orderByAscExpression;
    }
    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }
    protected void AddDistinct() => IsDistinct = true;
    protected void Pagination(int skip, int take)
    {
        Take = take;
        Skip = skip;
        IsPagingEnabled = true;
    }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
       if(Criteria != null)
            query = query.Where(Criteria);
       return query;
    }
}
public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
    : BaseSpecification<T>(criteria),ISpecification<T, TResult> {
    protected BaseSpecification() : this(null)
    {}
    public Expression<Func<T, TResult>>? Select { get; private set; }
    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}
