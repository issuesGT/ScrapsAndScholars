<template>
  <section id="container">
    <div class="parallax">
    </div>
    <h1>Scraps & Scholars, its our food</h1>
  </section>

  <article>
    <section class="container">
      <div class="box">
        <h2>01</h2>
        <h3>Food Sharing Platform</h3>
        <p>
          It’s all about making it easy to share extra food instead of letting it go to waste. Whether you’re giving away leftovers or picking up a meal, you’re helping someone out and making campus life a little better for everyone.
          <br><br>
          With simple logins, instant messages, and notifications that keep you in the loop, it’s safe, easy, and actually kind of fun. At the end of the day, it’s about more than just food it’s about looking out for each other.
        </p>
      </div>
    </section>
    <section class="container1">
      <div class="box1">
        <h2>02</h2>
        <h3>Credit-Based <br />Exchange System</h3>
        <p>
          Instead of paying with cash, students earn credits by donating food or helping out with deliveries. They can then use those credits to grab meals posted by others.
          <br><br>
          It’s a simple system that keeps things fair, encourages everyone to pitch in, and makes sure no one’s left out just because they’re low on funds. Everyone has something to give and something to gain.
        </p>
      </div>
    </section>
    <section class="container2">
      <div class="box2">
        <h2>03</h2>
        <h3>Student Courier Network</h3>
        <p>
          Students can sign up to deliver food to others on campus and earn credits for helping out. The system matches you with nearby jobs, making it easy to pitch in when it works for you.
          <br><br>
          With built in delivery tracking and confirmations, everything stays organized and reliable. It’s a small way to help and a great way to be part of something bigger.
        </p>
      </div>
    </section>
  </article>

  <main>
    <section class="chooseUs">
      <h4>Why choose us</h4>
      <h2>We help you to stay fed.</h2>
      <p>Hunger makes people cranky. We fix that, with food.</p>
    </section>
    <section class="ContainDev">
      <div class="dataAnalys">
        <i class="fa-solid fa-lightbulb"></i>
        <h2>Data analysis</h2>
        <p>Science confirms: hungry people are 92% more dramatic.</p>
      </div>
      <div class="Website">
        <i class="fa-solid fa-window-maximize"></i>
        <h2>Website</h2>
        <p>One app to rule your cravings. (And maybe your life.)</p>
      </div>
      <div class="Management">
        <i class="fa-solid fa-face-smile"></i>
        <h2>Content Management</h2>
        <p>Managed by real humans. Fueled by snacks.</p>
      </div>
      <div class="Mobility">
        <i class="fa-solid fa-mobile-screen-button"></i>
        <h2>Mobility</h2>
        <p>Hungry on the go? We’ve got you, wherever you roam.</p>
      </div>
    </section>
  </main>


</template>

<script>
  export default {
    name: 'HomePage',
  }
</script>

<style scoped>

  /*Main*/

  main {
    display:grid;
    grid-template-columns: 1fr 1fr;
    grid-template-rows:1fr;
    grid-template-areas:
        'text boxes';
    margin:5%;
    padding-bottom:5%;
  }

  .chooseUs {
    grid-area:text;
    display:inline-block;
    margin:auto;
  }

  .chooseUs > h4 {
    color: #6F8BA4;
    font-size: 16px;
    margin-bottom:1%;
  }

  .chooseUs > h2 {
    font-size: 46px;
    margin-top:0;
  }

  .chooseUs > p {
    color: #6F8BA4;
    font-size: 18px;
  }

  .ContainDev {
    display: grid;
    grid-area: boxes;
    grid-template-columns: 1fr 1fr;
    grid-template-rows: 1fr 1fr;
    width: 100%;
    grid-gap:25px;
    grid-template-areas:
      'data web'
      'content mob';
  }

  .dataAnalys, .Website, .Management, .Mobility {
    position: relative;
    background: #f9f9f9;
    padding: 50px 40px 60px;
    box-shadow: 0px 0px 80px 0px rgba(85, 128, 255, 0.2);
    border-radius: 20px;
    text-align: center;
    margin-right: 10%;
  }

  .dataAnalys > i, .Website > i, .Management > i, .Mobility > i {
    width: 100%;
    color: orange;
    font-size: 35px;
  }

    .dataAnalys > p, .Website > p, .Management > p, .Mobility > p {
      color:#6F8BA4;
    }

  .dataAnalys {
    grid-area: data;
    
  }

  .Website {
    grid-area: web;
    top:15%;
  }

  .Management {
    grid-area: content;
  }

  .Mobility {
    grid-area: mob;
    top: 15%;
  }


  #container {
    background-image: url("../assets/images/heroImage.jpg");
    background-attachment: fixed;
    background-position: center;
    background-size: cover;
    height: 100vh;
    width: 100%;
    color: #FFFFFF;
    display: flex;
    justify-content: center;
    align-items: center;
  }

    #container > .parallax {
      background-color: rgba(0, 0, 0, 0.6);
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
    }

    #container > h1 {
      z-index: 0;
      font-size: 60px;
      text-align: center;
      width: 35%;
      text-shadow: 1px 1px 2px black;
      font-family: 'Exo 2', sans-serif;
      animation: leftin2 2s;
    }

  @keyframes leftin2 {
    from {
      opacity: 0;
    }

    to {
      opacity: 100;
    }
  }

  /*Article*/

  article {
    margin: 5% 5%;
    font-family: 'exo 2', sans-serif, Helvetica, 'Times New';
    display: grid;
    height: max-content;
    grid-template-columns: 1fr 1fr 1fr;
    grid-auto-rows: 1fr;
    grid-template-areas:
      'box box1 box2';
  }

  .container {
    grid-area: box;
  }

  .container1 {
    grid-area: box1;
  }

  .container2 {
    grid-area: box2;
  }

  .container, .container1, .container2 {
    display: flex;
    flex-wrap: wrap;
    text-align: center;
    margin: 0% 5%;
    z-index: 1;
  }

    .container > .box, .box1, .box2 {
      position: relative;
      background: #f9f9f9;
      padding: 50px 40px 60px;
      box-shadow: 0 15px 45px rgba(0, 0, 0,0.1);
    }

      .container > .box::before, .box1::before, .box2::before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: #36454F;
        transform: scaleX(0);
        transform-origin: left;
        transition: transform 0.5s;
      }

      .container > .box:hover::before, .box1:hover::before, .box2:hover::before {
        transform: scaleX(1);
        transform-origin: left;
        transition: transform 0.5s;
      }

      .container > .box h2, .box1 h2, .box2 h2 {
        position: absolute;
        left: 40px;
        top: 60px;
        font-size: 4em;
        font-weight: 800;
        z-index: 1;
        opacity: 0.1;
        transition: 0.5s;
      }

      .container > .box:hover h2, .box1:hover h2, .box2:hover h2 {
        opacity: 1;
        color: #FFF;
        transform: translateY(-60px);
      }

      .container > .box h3, .box1 h3, .box2 h3 {
        position: relative;
        font-size: 1.5em;
        z-index: 2;
        color: #333;
        transition: 0.5s;
      }

      .container > .box p, .box1 p, .box2 p {
        position: relative;
        z-index: 2;
        color: #555;
        transition: 0.5s;
      }

      .container > .box:hover h3, .box1:hover h3, .box2:hover h3,
      .container > .box:hover p, .box1:hover p, .box2:hover p {
        color: #FFF;
      }
  @media screen and (max-width:1486px) {
    main {
      display: grid;
      grid-template-columns: 1fr 1.5fr;
      grid-template-rows: 1fr;
      grid-template-areas:
        'text boxes';
      margin: 5%;
      padding-bottom: 5%;
    }

    .ContainDev {
      margin:5%;
    }
  }

  @media screen and (max-width:1100px) {

    main {
      display: grid;
      grid-template-columns: 1fr;
      grid-template-rows: 1fr;
      grid-template-areas:
        'text'
        'boxes';
      margin: 5%;
      padding-bottom: 5%;
    }

    article {
      margin: 5% auto;
      width: 90%;
      grid-template-columns: 1fr 1fr;
      grid-template-rows: 1fr;
      grid-template-areas:
        'box box1'
        'box2 box2';
    }

    .container2 {
      margin-top: 5%;
    }

    #container > h1 {
      font-size: 60px;
      width: 80%;
    }
  }

  @media screen and (max-width:800px) {
    article {
      margin: auto;
      width: 80%;
      grid-template-columns: 1fr;
      grid-template-areas:
        'box'
        'box1'
        'box2';
    }

    .container, .container1, .container2 {
      width: 100%;
      padding: 5% 0%;
      margin: auto;
    }

    .box {
      grid-area: box;
    }

    .box1 {
      grid-area: box1;
    }

    .box2 {
      grid-area: box2;
    }

    #container > h1 {
      font-size: 40px;
      width: 80%;
    }

    .ContainDev {
      display: grid;
      grid-area: boxes;
      grid-template-columns: 1fr;
      grid-template-rows: 1fr 1fr;
      width: 100%;
      grid-gap: 25px;
      grid-template-areas:
        'data'
        'web'
        'content'
        'mob';
    }

    .Mobility {
      top: 0%;
    }

    .Website {
      top: 0%;
    }
  }
</style>
