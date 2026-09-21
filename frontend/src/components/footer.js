function Footer() {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="footer">
      <p>ExploreKosova &copy; {currentYear}</p>
      <p>Projekt universitar - Aplikacionet, Shërbimet dhe Teknologjitë e Bazuara në Lokacion</p>
    </footer>
  );
}

export default Footer;