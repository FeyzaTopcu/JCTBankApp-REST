using JCTBank.BLL.Abstract;
using JCTBank.DAL.Abstract;
using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.BLL.Concrete
{
    public class TransferManager : ITransferService
    {
        private ITransferDal _TransferDal;

    public TransferManager(ITransferDal TransferDal)
    {
        _TransferDal = TransferDal;
    }

    public void Create(Transfer entity)
    {
        _TransferDal.Create(entity);
    }

    public void Delete(Transfer entity)
    {
        _TransferDal.Update(entity);
    }

    public List<Transfer> GetAll(Expression<Func<Transfer, bool>> filter = null)
    {
        return _TransferDal.GetAll().ToList();
    }

    public Transfer GetById(int id)
    {
        return _TransferDal.GetById(id);
    }

    public void Update(Transfer entity)
    {
        _TransferDal.Update(entity);
    }
}
}
