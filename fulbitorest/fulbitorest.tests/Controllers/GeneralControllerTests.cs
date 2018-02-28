using fulbitorest.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace fulbitorest.tests.Controllers
{
    [TestClass]
    public class GeneralControllerTests
    {
        [TestMethod]
        public void ControllersHaveAuthorizeAttribute()
        {
            ValidateAttribute(new List<Type> {
                typeof(UserController),
                typeof(DataController),
                typeof(TeamfanController)
            }, typeof(AuthorizeAttribute));
        }


        private static void ValidateAttribute(IEnumerable<Type> types, Type attributeType)
        {
            foreach(var type in types)
            {
                ValidateAttribute(type, attributeType);
            }
        }
        private static void ValidateAttribute(Type type, Type attributeType)
        {
            Assert.IsNotNull(Attribute.GetCustomAttribute(type, attributeType), type.Name + " doesnt have " + attributeType.Name);
        }
    }
}
