using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TextAdventure;

namespace TextAdventureTests
{
    [TestFixture]
    public class EntityTests
    {
        Timeline timeline = new Timeline();

        //[Test]
        //public void IsDsicoveredMustBeTrueBeforeIsTaken()
        //{
        //    var sut = new Entity("entity, timeline);
        //    sut.IsDiscovered = false;
        //    sut.IsTakable = true;

        //    Assert.IsFalse(sut.IsTakable);

        //    sut.IsDiscovered = true;
        //    sut.IsTakable = true;

        //    Assert.IsTrue(sut.IsTakable);
        //}

        //[Test]
        //public void TakeReturnsEntityActionResult()
        //{
        //    var sut = new Item("TestThing", timeline);

        //    Assert.IsInstanceOf<ActionResult>(sut.Take(new Actor("doer", timeline)));
        //}

        //[Test]
        //public void TakeChecksIfCanBeTaken()
        //{
        //    var sut = new Entity(timeline);
        //    sut.IsDiscovered = true;
        //    sut.IsTakable = true;
        //    var actor = new Actor("doer", timeline);
        //    var result = sut.Take(actor);
                        
        //    Assert.IsTrue(result.IsSuccessful);

        //    sut.IsDiscovered = true;
        //    sut.IsTakable = false;
        //    actor = new Actor("doer", timeline);
        //    result = sut.Take(actor);

        //    Assert.IsFalse(result.IsSuccessful);
        //}

        //[Test]
        //public void TakeSetsPassedEntityAsOwner()
        //{
        //    var sut = new Entity(timeline);
        //    sut.IsDiscovered = true;
        //    sut.IsTakable = true;
        //    var actor = new Actor("doer", timeline);
        //    var result = sut.Take(actor);
                        
        //    Assert.AreEqual(sut.Owner, actor);
        //    Assert.IsTrue(result.IsSuccessful);
        //}

        //[Test]
        //public void TakeUnsuccessfulIfTakerAlreadyOwns()
        //{
        //    var sut = new Entity(timeline);
        //    sut.IsDiscovered = true;
        //    sut.IsTakable = true;
        //    var actor = new Actor("doer", timeline);
        //    sut.Owner = actor;
        //    var result = sut.Take(actor);

        //    Assert.IsTrue(result.Outcome.Contains("already owns"));
        //    Assert.AreEqual(sut.Owner, actor);
        //    Assert.IsFalse(result.IsSuccessful);
        //}


    }
}
