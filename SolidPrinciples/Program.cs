
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solid.Demo
{

      class Program
    {
        // on this code we have applied all the SOLID principles as srp in OrderProcessor class, ocp in IOrderSaver interface, lsp in Bird class hierarchy, isp in IOrderSaver, IorderDeleter and IorderReader interfaces and dip in OrderProcessor class constructor, ocp in OrderProcessor class process method, lsp in Bird class hierarchy ,isp in IOrderSaver, IorderDeleter and IorderReader interfaces and dip in OrderProcessor class constructor
    static void Main(string[] args)
        {
            var validator = new OrderValidator();
            var savers = new IOrderSaver[] { new DbOrderSaver(), new CacheOrderSaver() };
            var notifier = new Notifier();
            var processor = new OrderProcessor(validator, savers, notifier);
            processor.Process();
        }
    } 

    public class OrderProcessor
    {
    //    steps are validate order, save order, notify user
    // so in this class we have applied single responsibility principle by separating each step into its own class which has only one responsibility
    // this is mainly useful when we have to change any step in future we can do it without affecting other steps
    private readonly OrderValidator orderValidator;
    private readonly IOrderSaver[] orderSaver;
    private readonly Notifier notifier;
 
 public OrderProcessor(OrderValidator validator, IOrderSaver[] orderSaver , Notifier notifier)
        {
            this.orderValidator = validator;
            this.orderSaver = orderSaver;
            this.notifier = notifier;   
            
        }   
      public void Process()
        {  
            orderValidator.Validate();
// open closed principle is applied here as we can add new order savers without changing the existing code by implementing IOrderSaver interface and passing it to the constructor as array
            foreach(var item in orderSaver){
                item.Save(null);
            }
            notifier.Notify();
        }
    }

    public class OrderValidator
    {
        public void Validate(){
            
        }
    }

// interface segregation principle is applied here as we have created separate interfaces for each functionality of order saving, deleting and reading
// so that the classes which are implementing these interfaces will only implement the methods which they are interested in and not the methods which they are not interested in    

    public interface IOrderSaver
    {
       void Save(string order);
    }
    public interface IorderDeleter
    {
         void Delete(int id);

    }
    public interface IorderReader
    {   
             string Read(int id);
    }

    public class DbOrderSaver : IOrderSaver
    {
        public void Save(string order){
            
        }
    }

    public class CacheOrderSaver : IOrderSaver
    {
        public void Save(string order){
            
        }
    }   
    public class Notifier
    {
        public void Notify(){
            
        }
    }  
         

}
// dependency inversion principle is applied here as we are depending on abstractions (interfaces) rather than concrete implementations
// so that we can easily change the implementation of the interfaces without affecting the existing code for example we can add new order savers by implementing IOrderSaver interface and passing it to the constructor of OrderProcessor class which is useful when we have to add new order savers in future.
  